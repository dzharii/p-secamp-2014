<source lang="cs">
class PageObjectTest
{
    [Test]
    public void Can_buy_an_Album_when_registered()
    {
        // Обычно, конструкторы PageObject объектов не выполняют действий на странице. 
        // Они необходимы лишь для получения ссылки на объект.
        var registerUserPage = new RegisterUserPage();
        
        // Просто открывает страницу регистрации, при этом, 
        // кликая на все нужные ссылки по пути
        registerUserPage.Invoke();

        // Этот класс используется для передачи данных. 
        // Некоторые данные могут быть заполнены «по умолчанию», но об этом – позже        
        var newUserFromData = new UserFromDataData()
        {
            UserName = "HJSimpson",
            Password = "!2345Qwert",
        };

        // Момент заполнения и отправки формы
        registerUserPage.FillForm(newUserFromData);
        registerUserPage.Submit();
        
        // А следующий код выбирает товар из витрины, добавляет его в корзину 
        // и переходит на страницу оформления заказа. 
        var showCasePage = new ShowCasePage();
        showCasePage.Goto("Disco");
        showCasePage.SelectProduct("showCasePage");
        showCasePage.AddToCard();
        showCasePage.Checkout();

        var checkOutForm = new CheckOutForm();

        // .DefaultValues возвращает класс с уже заполненными данными по умолчанию. 
        // Если нас что-то неустраивает – всегда можно заменить. 
        var checkoutFromData = UserCheckoutFromData.DefaultValues;


        // Вот как раз это и не устраивает! А давайте JavaScript инъекцию добавим!
        checkoutFromData.Email = @"chunkylover53@aol.com<script type=""text/javascript"">
                                   /* <![CDATA[ */
                                   (function(){try{var s,a,i,j,r,c,l,b=document.getElementsByTagName(""script"");l=b[b.length-1].previousSibling;a=l.getAttribute('data-cfemail');if(a){s='';r=parseInt(a.substr(0,2),16);for(j=2;a.length-j;j+=2){c=parseInt(a.substr(j,2),16)^r;s+=String.fromCharCode(c);}s=document.createTextNode(s);l.parentNode.replaceChild(s,l);}}catch(e){}})();
                                    /* ]]> */
                                   </script>";

        CheckoutCompletePage  checkoutCompletePage = checkOutForm.Submit();

        Assert.IsTrue(checkoutCompletePage.GetPageTitle().Contains("Checkout Complete"));
    }
}
</source>