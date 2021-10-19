해당 프로젝트는 이상영의 공부용 개인 프로젝트입니다.

ProjectTest/CreateLink.bat 파일을 관리자 권한으로 실행하여 Protocol/Core 폴더를 유니티 프로젝트에 연결하여야 합니다.

==========================작업된 내용들==========================

[Common]
 - Server, Client 공용로직 처리 (폴더 참조를 이용한 처리)
 - 패킷 구조 작성

[Server]
 - SELECT 쿼리로 데이터 테이블 가져오기(공부용) -> 이후 Dapper ORM 적용
 - 커스텀한 Route 구조를 사용할수 있게 구조 작성 (RouteAction)

[Client]
 - Zenject, UniRx Asset 등록
 - UniRx 성능 부하 문제로 인해 UniRx 구조를 활용한 EventCommand / EventProperty 작성

==========================작업예정 메모==========================

[Common TODO]
 - CSV Parser 작업

[Server TODO]
 - DB INSERT / DELETE / UPDATE 쿼리 적용(공부용) -> 이후 Dapper ORM 적용
 - Transaction 보장 작업(변경점 동기화 / CommitAsync 함수 작성)
 - 로그인 작업
 - 샤딩 작업

[Client TODO]
 - HTTP 웹서버 통신 적용
 - UI 베이스 작업
 - Battle Flow 로직 작성
 - Google FaceBook IOS 로그인 작업
 - 번들 적용 (Unity 번들? Addressable?...)
