# SquidGame
+ Unity Engine을 사용하여 오징어게임의 징검다리 건너기 구현
+ 칸마다 랜덤으로 한쪽에는 강화 유리, 한쪽에는 일반 유리가 생기고 일반 유리를 밟을 시 바닥에 떨어짐
+ Unity에서 제공되는 UNet의 HLAPI로 LAN 멀티플레이 구현

## Start
+ 1명은 Host, 1명은 Client로 Host에게 접속
![2](https://user-images.githubusercontent.com/56538203/141608756-a0d70949-f717-42a5-a71c-3827e771c32f.PNG)

## How To Play
+ 기본적인 움직임 (WASD와 방향키로 이동, Space Bar로 점프)
![14](https://user-images.githubusercontent.com/56538203/141608813-f14f447d-3122-48f9-bd09-9c5f6665f274.PNG)
![15](https://user-images.githubusercontent.com/56538203/141608824-538c5b5d-b28d-41b4-a2ca-8b9acb045816.PNG)

+ 강화 유리를 밟았을 때
![SquidGame 2021-12-06 오후 7_57_36](https://user-images.githubusercontent.com/56538203/148529629-97c06431-f56a-46eb-8cd0-d46ab87ebcd4.png)

+ 일반 유리를 밟을 시 바닥에 떨어지고 사망
![SquidGame 2021-12-06 오후 7_57_41](https://user-images.githubusercontent.com/56538203/148529492-94711077-972e-47d1-ab0f-c5209c18962a.png)
![SquidGame 2021-12-06 오후 7_59_14](https://user-images.githubusercontent.com/56538203/148529504-c4e1d333-4564-4038-b1e1-df4e5b96d100.png)

+ 사망 했을 시 시작지점에서 부활
![SquidGame 2021-12-06 오후 8_00_12](https://user-images.githubusercontent.com/56538203/148529509-cb56471d-e3dc-4521-ae18-f9de3b1a9014.png)

+ 끝까지 도달하는 것이 목표
![image](https://user-images.githubusercontent.com/56538203/144629345-ff59cdab-1c1a-4acb-b487-6254708abccf.png)

