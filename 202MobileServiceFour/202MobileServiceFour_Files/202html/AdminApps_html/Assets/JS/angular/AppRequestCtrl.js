angular.module("Admin").controller("AppRequestCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.AppRequest = {};
    $scope.SelectedView = 1;
    $scope.ModalTitle;
    $scope.SelectedPage = 1;
    $scope.PageSize = 12;
    $scope.DeleteMessage = "";
    
    $scope.data ={
        appRequest:[{
            AppRequestID: 1, DateRequested: '01/01/2017', Business: { BusinessID: 1, BusinessName: 'Business', BusinessPhone: '(763)555-1111', BusinessEmail: 'Test@Test.com', BusinessAddress: '', BusinessHoursStart: '6am', BusinessHoursEnd: '8pm', WebsiteUrl: '', FacebookUrl: '', ImageGalleryUrl: '', Other: '', TypeOfBusiness: '', AppLink: '', IsPublic: false, User: { UserInfoID: 1, UserName: 'cpkolsta', Email: 'Test@Test.com', Name: 'Charles', Password: '', ConfirmPassword: '', ProfileImage: ''}, AppStatus: '', BusinessImage: '' }, DevStatus: 'New', FeaturesRequested: [{ FeatureRequestedID: 1, AppRequestID: 1, RequestedFeature: { FeatureName: 'Booking', Active: true}, DateRequested: '01/01/2017' }]}]
    };
    
    $scope.SetupAppRequest = function(request){
        $scope.AppRequest = request;
        $scope.SelectedView = 1;
        $scope.ModalTitle = 'Business Information';
        $scope.DeleteMessage = "";
        
        angular.element("#appRequestModal").modal('show');
    }
    
    $scope.DeleteRequeset = function(request){
        $scope.AppRequest = request;        
        $scope.DeleteMessage = "Are you sure you want to delete app request for " + request.Business.BusinessName + " created on " + request.DateRequested;
        $scope.SelectedView = 3;
        
        angular.element("#appRequestModal").modal('show');
    }
    
    $scope.CommitDelete = function(){
        $scope.AppRequest.AppRequestID;
        
        angular.element("#appRequestModal").modal('hide');
    }
    
    $scope.ChangeView = function(page){
        if(page == 1)
            $scope.ModalTitle = 'Business Information';
        else if(page == 2)
            $scope.ModalTitle = 'App Requested Features';
                
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