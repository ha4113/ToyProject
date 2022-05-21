using System;
using System.IO;
using System.Net;
using System.Text;
using Common.Core.Table;
using Common.Core.Table.Util;
using Common.Protocol.Attributes;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using Cysharp.Threading.Tasks;
using ModestTree;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using Zenject;

public interface IPacketManager
{
    IEventObservable<ResponseResult> ResponseError { get; }
}

public class PacketManager : IOutputSubscriber, IPacketManager, IInitializable
{

    public CompositeDisposable OutputDisposable { get; } = new();
    public IEventObservable<ResponseResult> ResponseError => _packetError;

    private readonly EventCommand<ResponseResult> _packetError = new();

    public void Initialize()
    {
        ReqTest().Forget();
        var a = TableContainer<ClientOnly>.Get(1);
    }
    
    public void Dispose()
    {
        _packetError?.Dispose();
    }
    
    private async UniTask ReqTest()
    {
        var ack = await Request<GetTestReq, GetTestAck>(new GetTestReq());
        Debug.Log($"Req Result : {ack.Result}, {ack.TestValue}");
    }
    
    private async UniTask<TAck> Request<TReq, TAck>(TReq req)
        where TReq : IReq
        where TAck : IAck
    {
        try
        {
            req.Id = 1; // TODO : 로그인된 유저정보를 통해 세팅
        
            // 기본값 세팅
            var attr = req.GetType().GetAttribute<ReqAttribute>();
            var reqJson = JsonConvert.SerializeObject(req);
            var byteDataParams = Encoding.UTF8.GetBytes(reqJson);
        
            // req 패킷 구성
            var webRequest = WebRequest.Create($"http://localhost:9061/{attr.Api}");
            webRequest.Headers.Add("format", "JSON");
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentLength = byteDataParams.Length;
        
            // 보낼 데이터 스트림에 쓰기
            var stDataParams = await webRequest.GetRequestStreamAsync(); 
            await stDataParams.WriteAsync(byteDataParams, 0, byteDataParams.Length); 
            stDataParams.Close();
        
            // 결과 수신 및 결과 스트림 읽기
            var response = await webRequest.GetResponseAsync();
            var sr = new StreamReader(response.GetResponseStream() ?? throw new NetException(ResponseResult.InvalidResponseStream));
            var json = await sr.ReadToEndAsync();
            var ack = JsonConvert.DeserializeObject<TAck>(json);
        
            if (ack.Result != ResponseResult.Success)
            {
                throw new NetException(ack.Result);
            }

            // TODO : Ack 내에 DB 변경사항 반영
            return ack;
        }
        catch (Exception e)
        {
            if (e is not NetException netException)
            {
                throw;
            }

            _packetError.Execute(netException.ErrorCode);
            return default;
        }
    }
}
