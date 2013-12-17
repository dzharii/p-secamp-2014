set TMPLOCATION=%TEMP%se-2014-presentation
rd /S /Q %TMPLOCATION%
md %TMPLOCATION%

xcopy presentation\*.* %TMPLOCATION%\*.* /sy
call git checkout gh-pages

xcopy %TMPLOCATION%\*.* *.* /sy

call git add *
call git commit -am "Presentation update GH Pages"

call git checkout master
call git add *
call git commit -am "Presentation update Master"


