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
	string frame; // ���a�����
	string direction; // ���a����V
};

int main() {
	string status = "";
	cout << "�M��J Start �}�l�C��" << endl;
	cin >> status;
	while (status == "Start")
	{
		bool isFrameWin = false;
		player myPlayer;
		player Enemy;
		string hand1 = "";
		string hand2 = "";
		while (true) {
			cout << "�п�J���Rock/Scissors/Paper" << endl;
			cin >> hand1; //�Ĥ@��Ū��
			//Sleep(3000);
			cout << "�п�J�ۦP���Rock/Scissors/Paper" << endl;
			cin >> hand2; //�ĤG��Ū��
			if (hand1 == hand2) {      //�P�_�⦸��դ@��
				break;
			}
		}
		myPlayer.setFrame(hand1);
		// ����H���X��
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
		// �P�_��Ĺ
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
		// Ū���⪺��V
		hand1 = "";
		hand2 = "";
		while (true) {
			cout << "�п�J��VUp/Down/Left/Right" << endl;
			cin >> hand1; //�Ĥ@��Ū��
			//Sleep(3000);
			cout << "�п�J�ۦP��VUp/Down/Left/Right" << endl;
			cin >> hand2; //�ĤG��Ū��
			if (hand1 == hand2) {      //�P�_�⦸��դ@��
				break;
			}
		}
		myPlayer.setDirection(hand1);

		// ���⪺��V
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
		// �P�_��V�O�_�@�P
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
		// ��J�O�_�n�A���@��
		Sleep(1000);
		system("cls");
		cout << "�п�J Start �}�l�C��" << endl;
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