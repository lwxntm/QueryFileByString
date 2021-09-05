#include <iostream>
#include "Tool.h"
#include <sys/timeb.h>
#include <ctime>

long long GetTimeCuoFromTimeb(timeb t)
{
	return t.time * 1000 + t.millitm;
}

int main(int argc, char* argv[])
{
	if (argc < 2) {
		cout << "请输入一个参数" << endl;
		return 0;
	}
	const char* currentDictionary = getCurrentDictionary();
	std::cout << "当前目录是：" << currentDictionary << std::endl;

	string filePath = currentDictionary;
	vector<string> files;
	//vector<string> filesname; 

	timeb t;
	ftime(&t);//获取毫秒


	//获取该路径下的所有文件路径  
	getFiles1(filePath, files, argv[1]);
	//获取该路径下的所有文件路径和文件名 
	//getFiles2(filePath, files, filesname);

	timeb t1;
	ftime(&t1);//获取毫秒
	long long time = GetTimeCuoFromTimeb(t1) - GetTimeCuoFromTimeb(t);

	char str[30];
	for (int i = 0; i < files.size(); i++)
	{
		cout << files[i].c_str() << endl;
	}
	cout << "耗时" << time << "毫秒";

	return 0;
}