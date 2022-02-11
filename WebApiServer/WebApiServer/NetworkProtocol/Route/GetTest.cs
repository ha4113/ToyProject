using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Core.Table;
using Common.Core.Table.Util;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using Serilog;
using Server.DBProtocol;
using Server.DBProtocol.Schema;

namespace Server.NetworkProtocol.Route
{
    public class GetTest : RouteAction<GetTestReq, GetTestAck>
    {
        protected override async Task<GetTestAck> Run(GetTestReq req, User user)
        {
            // 코드 파일에 다음 using 지시문을 추가합니다.  
            // using System.Linq.Expressions;  

            // 람다 식에 대한 식 트리를 수동으로 빌드 num => num < 5.  
            var numParam = Expression.Parameter(typeof(int), "num");
            var five = Expression.Constant(5, typeof(int));
            var numLessThanFive = Expression.LessThan(numParam, five);
            var lambda1 = Expression.Lambda<Func<int, bool>>(numLessThanFive, numParam);

            // 매개변수 표현식 작성
            var value = Expression.Parameter(typeof(int), "value");

            // 지역 변수를 담을 표현식 만들기.
            var result = Expression.Parameter(typeof(int), "result");

            // 루프에서 이동할 레이블 만들기.  
            var label = Expression.Label(typeof(int));
            var test = DefMgr<DefTest>.Get(1);
            Console.WriteLine(test.Name);
            // 메서드 본문 만들기.  
            var block = Expression.Block(
                // 지역 변수 추가.  
                new[] { result },
                // 지역 변수에 상수 할당: result = 1
                Expression.Assign(result, Expression.Constant(1)),
                // 루프 추가  
                Expression.Loop(
                    // 루프에 조건부 블록을 추가합니다  
                    Expression.IfThenElse(
                        // 조건: value > 1
                        Expression.GreaterThan(value, Expression.Constant(1)),
                        // 만약 true 이면: result *= value --
                        Expression.MultiplyAssign(result, Expression.PostDecrementAssign(value)),
                        // 거짓이면 루프를 종료하고 레이블로 이동합니다.  
                        Expression.Break(label, result)
                    ),
                    // 이동할 레이블입니다.
                    label
                )
            );

            // 표현식 트리 컴파일
            var factorial = Expression.Lambda<Func<int, int>>(block, value).Compile();

            // 표현식 트리 실행
            Console.WriteLine(factorial(5));
            // 120 출력  


            var account = await user.GetModelAsync<Account>();
            return new GetTestAck(ResponseResult.Success, account.WakeTimeType);
        }
    }
}