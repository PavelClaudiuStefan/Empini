<?xml version="1.0" encoding="UTF-8"?>
<tileset name="lava" tilewidth="32" tileheight="32" tilecount="18" columns="3">
 <image source="../tiles/lava.png" width="96" height="192"/>
 <terraintypes>
  <terrain name="Lava" tile="10"/>
 </terraintypes>
 <tile id="1" terrain="0,0,0,">
  <objectgroup draworder="index">
   <object id="3" x="0" y="0">
    <polygon points="0,0 32,0 31.25,21.25 28.75,24.75 25,25 24.25,31.5 0,32"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="2" terrain="0,0,,0">
  <objectgroup draworder="index">
   <object id="1" x="32" y="0">
    <polygon points="0,0 0,32 -25,31.5 -32.25,19.75 -32,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="4" terrain="0,,0,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="32">
    <polygon points="0,0 0,-32 24.75,-32 23.75,-25 31.5,-22.75 32,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="5" terrain=",0,0,0">
  <objectgroup draworder="index">
   <object id="1" x="32" y="32">
    <polygon points="0,0 0,-32 -24.25,-32.5 -25,-25 -32.5,-23.5 -32,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="6" terrain=",,,0">
  <objectgroup draworder="index">
   <object id="4" x="7.75" y="31.5">
    <polygon points="0,0 5.75,-6.25 7.75,-12.5 13.25,-17.75 21,-23 24.5,-24 24,0.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="7" terrain=",,0,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="32">
    <polygon points="0,0 0.25,-23.5 11.25,-23.25 16.75,-26.75 24,-26.25 26,-25 31.75,-24 32,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="8" terrain=",,0,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="7.75">
    <polygon points="0,0 6.25,0.25 12.25,3.5 13.75,8.5 17,10 20.25,15.5 22,19.25 23,23.5 0,24.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="9" terrain=",0,,0">
  <objectgroup draworder="index">
   <object id="1" x="9" y="0">
    <polygon points="0,0 23,0 23,32 -0.75,31.75 2.75,21.5 1.25,10.75"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="10" terrain="0,0,0,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="11" terrain="0,,0,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 23.5,-0.25 17.5,7.5 26.75,22.5 23,32.25 0,32"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="12" terrain=",0,,">
  <objectgroup draworder="index">
   <object id="1" x="8" y="0">
    <polygon points="0,0 24,0 23.25,21 12.5,15.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="13" terrain="0,0,,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="19.75">
    <polygon points="0,0 0,-19.75 32,-19.75 32,0.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="14" terrain="0,,,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 22.75,0 17.5,13.75 7.5,21.75 -0.5,21.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="15">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="16">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="17">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
</tileset>
