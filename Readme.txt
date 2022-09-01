I created this project with Unity 2021.3.5 LTS version. If it's not already open, you can open game scene with Assests/Scenes/GameScene.

I tried to make project in object in object oriented way. You can find directory of classes under Scripts folder. 
There are Buildings, Stackables and StackableFields. 

StackableField classes means they are spreaded into terrain and you can pick them 
while walking. StackableField classes place stackable plants precalculated places, activate them in random seconds which specified in scriptable objects.

Stackable Classes manages stacking operation and manager class for it StackableSystem. I add StackableSystem to player and StorageArea building.

These two systems, StackableField and StackableSystem, can have many items so i used ObjectPool with them.

And there is also building classes. 

I used scriptable objects for data storing and I placed all of them under the ScriptableData folder.

Typical game scenerio: You can wait plants to grow and go to them and stack them. 
Then you can give them to the bank with entering bank area. Every building has area for interaction between the hedges. 
Bank give you money and money is green cube. And you can buy bakery or storage place. 
After buying bakery it is giving you money with repeating in specifed time in scriptable objects.
And you can buy storing area building and you can store your money or plants there.
Bakery has own storing area.