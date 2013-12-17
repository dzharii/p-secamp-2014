set TMPLOC=%TEMP%\SECONF2014

call git add *
call git commit -am "Presentation update MASTER"

xcopy presentation\*.* %TMPLOC%\*.* /sy

call git checkout gh-pages

xcopy %TMPLOC%\*.* *.* /sy

call git add *
call git commit -am "Presentation update GH-PAGES"

call git checkout master

call git push origin master 
call git push origin gh-pages



