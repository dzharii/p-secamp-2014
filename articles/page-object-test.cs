<source lang="cs">
class PageObjectTest
{
    [Test]
    public void Can_buy_an_Album_when_registered()
    {
        // ������, ������������ PageObject �������� �� ��������� �������� �� ��������. 
        // ��� ���������� ���� ��� ��������� ������ �� ������.
        var registerUserPage = new RegisterUserPage();
        
        // ������ ��������� �������� �����������, ��� ����, 
        // ������ �� ��� ������ ������ �� ����
        registerUserPage.Invoke();

        // ���� ����� ������������ ��� �������� ������. 
        // ��������� ������ ����� ���� ��������� ��� ����������, �� �� ���� � �����        
        var newUserFromData = new UserFromDataData()
        {
            UserName = "HJSimpson",
            Password = "!2345Qwert",
        };

        // ������ ���������� � �������� �����
        registerUserPage.FillForm(newUserFromData);
        registerUserPage.Submit();
        
        // � ��������� ��� �������� ����� �� �������, ��������� ��� � ������� 
        // � ��������� �� �������� ���������� ������. 
        var showCasePage = new ShowCasePage();
        showCasePage.Goto("Disco");
        showCasePage.SelectProduct("showCasePage");
        showCasePage.AddToCard();
        showCasePage.Checkout();

        var checkOutForm = new CheckOutForm();

        // .DefaultValues ���������� ����� � ��� ������������ ������� �� ���������. 
        // ���� ��� ���-�� ������������ � ������ ����� ��������. 
        var checkoutFromData = UserCheckoutFromData.DefaultValues;


        // ��� ��� ��� ��� � �� ����������! � ������� JavaScript �������� �������!
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