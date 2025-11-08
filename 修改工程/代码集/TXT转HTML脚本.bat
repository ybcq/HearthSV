@echo off 
set utf8=65001
set ansi=936
chcp %utf8%      
for /f "delims=" %%i in ('dir /b *.txt') do (
findstr /n .* "%%i" >.tmp
(

echo ^<html^>
echo ^<head^>
echo ^<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /^>
echo ^<meta name="GENERATOR" content="Microsoft FrontPage 4.0" /^>
echo ^<meta name="ProgId" content="FrontPage.Editor.Document" /^>
setlocal enabledelayedexpansion
for /r %%a in (*.tmp) do (    
set /p Str=<"%%~a"
echo ^<title^>%%~ni^</title^> 
)
EndLocal                
echo ^</head^>
echo ^<body^>
setlocal enabledelayedexpansion
for /r %%a in (*.tmp) do (    
set /p Str=<"%%~a"
echo ^<h1^>%%~ni^</h1^> 
)
EndLocal 
for /f "skip=1 delims=" %%i in (.tmp) do (
set str=%%i
SetLocal EnableDelayedExpansion
echo !str:*:=!^<br^>
EndLocal
)
echo ^</body^>
echo ^</html^>
) > %%~ni.html
)

del .tmp