import os

temp = os.walk('./training_dataset', topdown=False)
for root, dirs, files in temp:
    for i in dirs:
        dir = os.path.join(root,i)
        os.rename(dir, dir.lower())
