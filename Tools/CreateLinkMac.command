#!/usr/bin/env python

import os
import sys
import subprocess


pathname = os.path.dirname(os.path.abspath(__file__))
print "current path : " + pathname

link_list_file = open(pathname + "/LinkList.txt");
lines = link_list_file.readlines();


for rawline in lines:
    line = rawline.rstrip('\n').rstrip('\r').replace("\\", "/")
    print "parsing line : " + line
    tokens = line.split(",")
    src = pathname + "/" + tokens[1]
    dst = pathname + "/" + tokens[0]
    print "src : " + src
    print "dst : " + dst
    subprocess.call("rm -rf \"" + dst + "\"", shell=True)
    subprocess.call("ln -s -f \"" + src + "\" \"" + dst + "\"", shell=True)
