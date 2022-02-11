<Copyright 2021. 이상영. All rights reserved.>

---
최초 설정
---
아래 툴을 실행하여 각종 폴더들을 연결하여야 한다
 - Tools/CreateLinkWin.bat(Windows OS)
 - Tools/CreateLinkMac.command (Mac OS)

---
작업 완료
---

### Common
 - Server, Client 공용로직 처리 (Symbolic Link 를 이용한 처리)
 - 패킷 구조 작성 완료
 - CSV Parser 작업 완료
 - 테이블데이터 글로벌 접근 작업 완료

### Server
 - SELECT 쿼리로 데이터 테이블 가져오기(공부용) -> 이후 Dapper ORM 적용
 - 커스텀한 Route 구조를 사용할수 있게 구조 작성 (RouteAction)

### Client
 - Zenject, UniRx Asset 등록 완료
 - UniRx 성능 부하 문제로 인해 UniRx 구조를 활용한 EventCommand / EventProperty 작성 완료
 - 패킷 연결 작업 완료
 - input output event 작업 완료

---
작업 예정
---

### Common TODO
 - 테이블 검증기 작업

### Server TODO
 - DB INSERT / DELETE / UPDATE 쿼리 적용(공부용) -> 이후 Dapper ORM 적용
 - Transaction 보장 작업(변경점 동기화 / CommitAsync 함수 작성)
 - 로그인 작업
 - 샤딩 작업

### Client TODO
 - UI 베이스 작업
 - Battle Flow 로직 작성
 - Google FaceBook IOS 로그인 작업
 - 번들 적용 (Unity 번들? Addressable?...)
