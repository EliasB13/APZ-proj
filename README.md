# GetIt
Complex system for common usage items tracking (e.g. tracking tools, devices, items, sharing services etc). This system allows businesses to register their common usage items, put RFID-tags on them, and see if this item available, who use it, and check if person has returned it. 

SYSTEM ARCHITECTURE
-------------------
The system consists of 4 subsystems:
1. Frontend (Vue JS + Vuex)
2. Backend (ASP.NET Core 3.0 + MSSQL Server DB)
3. Mobile (Kotlin for Android 5.0)
4. IoT-solution (C# console mock that is emulating IoT-device)  
  
*Source code for every subsystem is located on __dev__ branch under corresponding folders*

DEMO
----
You can try system demo by clicking this [***link***](https://getit13.herokuapp.com)

To sign in you can use sample credentials: 
* Business user  
  Login: **SampleBusinessAccount**  
  Pass: **#SampleBusAcc13**
    
* Private user  
  Login: **SamplePrivateAccount**  
  Pass: **#SamplePrivAcc13**
  
*Or you can create a new account.*
