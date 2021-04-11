##Custom Attributes

Tested on Unity 2020.3
License: [GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/ "GNU GPLv3")

CustomAttributes is a very simple extension for the Unity Inspector.
In order to keep the scripts simple and increase the chance of older and future unity versions compatability, I made sure no attribute script inherits from any outside scripts or core extensions and are all compelety independent, but also limited to the basic attribute features.

All attributes are made with unity `CustomPropertyDrawer` feature, for refrence see [here](https://docs.unity3d.com/Manual/editor-PropertyDrawers.html "here").

#####System Requirements

Tested on unity 2020.3, but will most likely work on on any version after 2018.
Dont forget to use the CustomAttributes namespace.
###Installation:
Installation can either be done using the git URL:
	`https://github.com/Phoder1/com.alontalmi.customattributes.git`
1. By clicking on the + sign in the unity package manager, selecting:
	"Add package from git URL" and pasting the git URL. 
2. By adding to your manifest.json file in your project package folder the following line:

`"com.alontalmi.customattributes": "https://github.com/Phoder1/com.alontalmi.customattributes.git"`

Support

Custom Attributes is an open-source project so feel free to offer improvement, open issues and share the project.
