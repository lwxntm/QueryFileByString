import os
import sys
import time


def QueryFile(queryString, nowFolder="."):
    # 列出当前文件夹下的所有文件
    fileList = os.listdir(nowFolder)
    # 遍历当前文件下的文件，把文件名包含所需字符串的文件的加入列表
    filteredList = [os.path.join(nowFolder, x) for x in fileList if
                    (x.find(queryString) != -1) and (os.path.isfile(os.path.join(nowFolder, x)))]
    # 获取当前文件夹下的子文件夹
    subFolders = [x for x in fileList if (os.path.isdir(os.path.join(nowFolder, x)))]
    for subFolder in subFolders:
        # windows系统跳过没有权限的管理员文件夹
        if subFolder.find("System Volume Information") > -1 or subFolder.find("RECYCLE.BIN") > -1:
            continue
            # 遍历子文件夹，结果拼接在一起
        filteredList.extend(QueryFile(queryString, nowFolder=os.path.join(nowFolder, subFolder)))
    return filteredList


if __name__ == '__main__':
    # 获取第一个参数，查找
    if len(sys.argv) > 1:
        T1 = time.perf_counter()
        result = QueryFile(sys.argv[1])
        T2 = time.perf_counter()
        for i in result:
            print(i)
        print(f"查找{sys.argv[1]} 共{len(result)}条结果，耗时{T2 - T1}")
