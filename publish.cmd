set TMPLOC=%TEMP%\SECONF2014

call git add *
call git commit -am "Presentation update MASTER"

xcopy presentation\*.* %TMPLOC%\*.* /sy
copy publish_part2.bat %TMPLOC%\publish_part2.bat /y

%TMPLOC%\publish_part2.bat





