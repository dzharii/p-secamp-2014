call git checkout gh-pages

xcopy %TMPLOC%\*.* *.* /sy

call git add *
call git commit -am "Presentation update GH-PAGES"

call git checkout master

call git push origin master 
call git push origin gh-pages