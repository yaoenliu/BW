#include <iostream>
#include <windows.h>
#include <stdlib.h>

using namespace std;

class player {
public:
	player();
	void setFrame(string);
	void setDirection(string);
	string getHand();
	string getDirection();
private:
	string frame; // 玩家的手勢
	string direction; // 玩家的方向
};

int main() {
	string status = "";
	cout << "清輸入 Start 開始遊戲" << endl;
	cin >> status;
	while (status == "Start")
	{
		bool isFrameWin = false;
		player myPlayer;
		player Enemy;
		string hand1 = "";
		string hand2 = "";
		while (true) {
			cout << "請輸入手勢Rock/Scissors/Paper" << endl;
			cin >> hand1; //第一次讀取
			//Sleep(3000);
			cout << "請輸入相同手勢Rock/Scissors/Paper" << endl;
			cin >> hand2; //第二次讀取
			if (hand1 == hand2) {      //判斷兩次手勢一樣
				break;
			}
		}
		myPlayer.setFrame(hand1);
		// 對手隨機出拳
		int enemyFrame = rand() % 3;
		if (enemyFrame == 0) {
			cout << "Enemy:Rock" << endl;
			Enemy.setFrame("Rock");
		}
		else if (enemyFrame == 1) {
			cout << "Enemy:Scissors" << endl;
			Enemy.setFrame("Scissors");
		}
		else {
			cout << "Enemy:Paper" << endl;
			Enemy.setFrame("Paper");
		}
		// 判斷輸贏
		if (myPlayer.getHand() == "Rock") {
			if (Enemy.getHand() == "Rock") {
				cout << "Draw" << endl;
				continue;
			}
			else if (Enemy.getHand() == "Scissors") {
				cout << "Win" << endl;
				isFrameWin = true;
			}
			else {
				cout << "Lose" << endl;
				isFrameWin = false;
			}
		}
		else if (myPlayer.getHand() == "Scissors") {
			if (Enemy.getHand() == "Rock") {
				cout << "Lose" << endl;
				isFrameWin = false;
			}
			else if (Enemy.getHand() == "Scissors") {
				cout << "Draw" << endl;
				continue;
			}
			else {
				cout << "Win" << endl;
				isFrameWin = true;
			}
		}
		else {
			if (Enemy.getHand() == "Rock") {
				cout << "Win" << endl;
				isFrameWin = true;
			}
			else if (Enemy.getHand() == "Scissors") {
				cout << "Lose" << endl;
				isFrameWin = false;
			}
			else {
				cout << "Draw" << endl;
				continue;
			}
		}
		// 讀取手的方向
		hand1 = "";
		hand2 = "";
		while (true) {
			cout << "請輸入方向Up/Down/Left/Right" << endl;
			cin >> hand1; //第一次讀取
			//Sleep(3000);
			cout << "請輸入相同方向Up/Down/Left/Right" << endl;
			cin >> hand2; //第二次讀取
			if (hand1 == hand2) {      //判斷兩次手勢一樣
				break;
			}
		}
		myPlayer.setDirection(hand1);

		// 對手手的方向
		int enemyDirection = rand() % 4;
		if (enemyDirection == 0) {
			cout << "Enemy:Up" << endl;
			Enemy.setDirection("Up");
		}
		else if (enemyDirection == 1) {
			cout << "Enemy:Down" << endl;
			Enemy.setDirection("Down");
		}
		else if (enemyDirection == 2) {
			cout << "Enemy:Left" << endl;
			Enemy.setDirection("Left");
		}
		else {
			cout << "Enemy:Right" << endl;
			Enemy.setDirection("Right");
		}
		// 判斷方向是否一致
		if (myPlayer.getDirection() != Enemy.getDirection()) {
			system("cls");
			cout << "Next Round Again" << endl;
			continue;
		}

		if (isFrameWin) {
			cout << "Win" << endl;
		}
		else {
			cout << "Lose" << endl;
		}
		// 輸入是否要再玩一次
		Sleep(1000);
		system("cls");
		cout << "請輸入 Start 開始遊戲" << endl;
		cin >> status;
	}
}
player::player() {
	frame = "";
	direction = "";
}
void player::setFrame(string hand) {
	frame = hand;
}
void player::setDirection(string hand) {
	direction = hand;
}
string player::getHand() {
	return frame;
}
string player::getDirection() {
	return direction;
}