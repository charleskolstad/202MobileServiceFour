angular.module("Admin").controller("BusinessCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.Business = {};
    $scope.SelectedView = 1;
    $scope.SelectedPage = 1;
    $scope.PageSize = 12;
    
    $scope.data ={
        Business: [{ 
            BusinessID: 1, BusinessName: 'Business', BusinessPhone: '(763)555-1111', BusinessEmail: 'Test@Test.com', BusinessAddress: '', BusinessHoursStart: '6am', BusinessHoursEnd: '8pm', WebsiteUrl: '', FacebookUrl: '', ImageGalleryUrl: '', Other: '', TypeOfBusiness: '', AppLink: '', IsPublic: false, User: { UserInfoID: 1, UserName: 'cpkolsta', Email: 'Test@Test.com', Name: 'Charles', Password: '', ConfirmPassword: '', ProfileImage: ''}
        }]
    };
    
    $scope.SetupBusiness = function(business){
        $scope.Business = business;
        $scope.SelectedView = 1;
        
        angular.element("#businessModal").modal('show');
    }
    
    $scope.ChangeView = function(page){
//        if(page == 1)
//            $scope.ModalTitle = 'Business Information';
//        else if(page == 2)
//            $scope.ModalTitle = 'App Requested Features';
                
        $scope.SelectedView = page;
    }
    
    $scope.ShowView = function(page){
        if(page == $scope.SelectedView)
            return true;
        else
            return false;
    }
    
    //pagination    
    $scope.SelectPage = function(newPage){
        $scope.SelectedPage = newPage;
    }
});