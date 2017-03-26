<?xml version="1.0" encoding="UTF-8"?>
<tileset name="water" tilewidth="32" tileheight="32" tilecount="18" columns="3">
 <image source="../tiles/water.png" width="96" height="192"/>
 <terraintypes>
  <terrain name="Water" tile="10"/>
 </terraintypes>
 <tile id="0">
  <objectgroup draworder="index">
   <object id="2" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="1" terrain="0,0,0,">
  <objectgroup draworder="index">
   <object id="1" x="0" y="32">
    <polygon points="0,0 0,-32 32,-32 32,-16.25 17.75,-10.75 15.25,-0.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="2" terrain="0,0,,0">
  <objectgroup draworder="index">
   <object id="2" x="0" y="0">
    <polygon points="0,0 32,0 32,32 17.25,31.25 15,23 5.25,15 -0.25,15.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="3">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
  <animation>
   <frame tileid="15" duration="1000"/>
   <frame tileid="16" duration="1000"/>
   <frame tileid="17" duration="1000"/>
  </animation>
 </tile>
 <tile id="4" terrain="0,,0,0">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 0,32 32,32 31.75,18 17.75,15.25 14.5,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="5" terrain=",0,0,0">
  <objectgroup draworder="index">
   <object id="1" x="32" y="0">
    <polygon points="0,0 0,32 -32,32 -32,19.75 -21,17.25 -14.75,-0.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="6" terrain=",,,0">
  <objectgroup draworder="index">
   <object id="2" x="32" y="32">
    <polygon points="0,0 -0.5,-16.5 -15,-0.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="7" terrain=",,0,0">
  <objectgroup draworder="index">
   <object id="2" x="32" y="32">
    <polygon points="0,0 -32,0 -31.5,-14 -0.25,-12.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="8" terrain=",,0,">
  <objectgroup draworder="index">
   <object id="2" x="0" y="32">
    <polygon points="0,0 0,-16.25 15.25,-0.5"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="9" terrain=",0,,0">
  <objectgroup draworder="index">
   <object id="2" x="32" y="0">
    <polygon points="0,0 0,32 -12.75,31.75 -12.25,0.25"/>
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
   <object id="2" x="0" y="0">
    <polygon points="0,0 0,32 15.25,31.5 12,15.5 11.75,-0.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="12" terrain=",0,,">
  <objectgroup draworder="index">
   <object id="1" x="32" y="0">
    <polygon points="0,0 -0.5,15.75 -15.5,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="13" terrain="0,0,,">
  <objectgroup draworder="index">
   <object id="1" x="32" y="0">
    <polygon points="0,0 -32,0 -31.75,14.25 -13.75,12.5 -0.5,12.25"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="14" terrain="0,,,">
  <objectgroup draworder="index">
   <object id="4" x="0" y="0">
    <polygon points="0,0 15.5,0.5 0,16"/>
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
