# BEV3 `(v3.0)`
BasicEventViewer BEV3 

 Basic Event Viewer v3.0 For Windows Vista and Windows Server 2008 , .NET Framework 3.5 and in may 2022 Updated for ver (4.5) , Published by Damon Mohammadbagher.
 
 Note: I Created this code in 2009 & in May 2022 Updated by me for Work with Win7/Win10 etc (working with .NetFramework 4.5),
 also Some Security Features Like Working with "ETWProcessMon2/ETWPM2Monitor2/Sysmon Logs & MITRE ATT&CK" will add this Source Code soon! ;)
 
 useful for Windows Admins also All Blue Teamers too.
 
 this code still has some problem for loading Large Event log records like 400,000 records etc , time to loading is very slow (not fast) for these large records
 so i need to work on this for better performance , with new codes and i will update source codes soon for fix this problem also i will add
 new security features to the source code (BEV v4.0).
 
 Video1 for BEV4: https://www.youtube.com/watch?v=imU82TApG2k
 
 Video2 for BEV4: https://www.youtube.com/watch?v=Hera3z1T5mI
 
 Important point: About Mitre Attack Detections This code [BEV4 (v4.0)] is/was my test codes (which i will publish here....) to use some Mitre Attacks Techniques (Using Atomic red-team) yaml file just for test & help to Blue teamers to learn these things better, BEV4 test is/was very good in my opinion but ofcourse i know this will not Detect every thing but this is very good example to start for Attack Detection based on Some Mitre Attack Data (using Sysmon EID 1 ONLY in my code) + yaml file & .... , you can see every Technique has steps (Procedures) which in my code BEV4 these steps or Procedures will detect by Sysmon EID 1 (commandLine) or ETW events (CommandLine) etc, in my code i created one simple "Techniques Database" which has all steps (Procedures) for each Technique (created base on yaml file for Atomic-Red-Team). that means in my DB i have technique A with 3 lines CommandLine and my code will Detected
each commandline and scan them for detection, and make score for each detection but this is not
enough for very good detection (which i learned this when i make this BEV4.0 ;D ) , because always you can bypass some detection very simple (sometimes), so if you think yaml file or Mitre Attack is enough always, you are wrong (i am sure 100% about this even before make this code) but Mitre Attack is very useful thing in my opinion and as i said before "Mitre Attack is/Can not Cover Everything...", anyway this code was fun also i learned alot from yaml files (atomic-red-team) + Mitre Attack things but i am sure this (my code) will not cover all things etc this just is for test.... also about Steps (Procedures) for Each Techniques you always can bypass detection, why? because always you can! 
so about "Techniques vs Procedures" this video is very good and i am agree with almost all things in this video honestly i don't know this guy (which does not matter), but this video is very good/useful for those Red teamers/Pentesters which want to work with Blue teamers to make something for Detection (Purple Teaming) also this video is very good for all blue teamers too. btw i will publish my code and hopes to helpful for some of you to make your own detection better than before ;) why not?

video link : https://youtu.be/MHfGIY2IyXE?t=414
 
 
 md5 info: 
      
       e12f3a0e2de2ca069cf58fd3292328b2 => BEV.exe [v3.0.77.22] 01,Jun,2022
       
### BEV v4.0 , Real-time Scan for Mitre-Attack Techniques for Defenders.  
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/MItreRealTime2.png)
   
### BEV v4.0 , new Search via Mitre Attack items for Defenders. (Loading yaml files for Search Sysmon Events for Mitre-Attack Techniques) 
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/MITREATTACK.png)
   
### BEV v4.0 , new Search via Mitre Attack items for Defenders. (search history in Sysmon Events for Mitre-Attack Techniques) 
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/MITREATTACK2.png)   
   
### BEV3 v3.0 , Filtering
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/BEV5.png)
       
### BEV3 v3.0 , Event Logs
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/BEV1.png)

### BEV3 v3.0 , Filtering Event Logs (Security log, Filtering for Windows Security Log)
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/BEV2.png)
   
### BEV3 v3.0 , Filtering Event Logs
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/BEV3.png)
   
### BEV3 v3.0 , Filtering Event Logs
   ![](https://github.com/DamonMohammadbagher/BEV3/blob/main/Images/BEV4.png)
   
   
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/BEV3/"/></a></p>
