// EC1011301  Lab01 Unit Conversion 
// B11132042, ¿à«Û§Ó 
// Sept. 14, 2024 
//
// Transfer the unit to US Customary units
#include<iostream>//Header defines standard input/output stream objects

using namespace std;//using standard namespace, i.e.,cin, cout

int main() {//Beginning of main program
	int input;//the data type of the input is integer

	cout << "Input length in centimeter: ";//the information for user

	cin >> input;//input the number

	cout << "Length in US Customary units: " << int(input / 2.54) / 12 << " feet " << int(input / 2.54) % 12 << " inches";// Transfer the unit to US Customary units
}