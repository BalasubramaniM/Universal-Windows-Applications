Universal-Windows-Application
=============================

Source : http://blogs.msdn.com/b/windowsappdev/archive/2013/03/13/alive-with-activity-part-3-push-notifications-and-windows-azure-mobile-services.aspx

1. First register your app to get credentials at the following link.

http://msdn.microsoft.com/en-in/library/windows/apps/hh465407.aspx

2. Now you have, ClientSecret, SID Package with you. These two are considered throughout this application.

3. Create new project in Visual Studio, select Universal app and create your application.

4. Right click your Windows 8.1 project file and select Associate your app with store option and associate. This will give access to authenticate your Windows 8.1 app. Refer following image.

![My image](https://github.com/BalasubramaniM/Images/blob/master/Windows8.png)


Follow same procedure for Windows Phone project too, since you are working on Universal app and also you should authenticate Windows Phone app with WNS service. Reference image below.

![My image](https://github.com/BalasubramaniM/Images/blob/master/WindowsPhone.png)

Now create WebSite to Create WNS service.

Select Website in NewProject in Vs. Refer following image.

![My image](https://github.com/BalasubramaniM/Images/blob/master/Website.png)

select Default Asp page, open .cs file and write functions to authenticate. Refer project for more info.

Note that, its local website only for your reference. Host it 
