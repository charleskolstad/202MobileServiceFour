angular.module("Admin").controller("BusinessCtrl", function ($http, $scope, dataUrl) {
    $scope.data = {};
    $scope.Business = {};
    $scope.SelectedPage = 1;
    $scope.PageSize = 12;
    $scope.Edit = true;
    $scope.DeleteMessage;
    
    $scope.data ={
        Business: [{ 
            BusinessID: 1, BusinessName: 'Business', BusinessPhone: '(763)555-1111', BusinessEmail: 'Test@Test.com', BusinessAddress: '', BusinessHoursStart: '6am', BusinessHoursEnd: '8pm', WebsiteUrl: '', FacebookUrl: '', ImageGalleryUrl: '', Other: '', TypeOfBusiness: '', BusinessImage: 'Assets/IMG/prudence-earl-599273-unsplash.jpg', AppLink: '', IsPublic: true, User: { UserInfoID: 1, UserName: 'cpkolsta', Email: 'Test@Test.com', Name: 'Charles', Password: '', ConfirmPassword: '', ProfileImage: ''}
        }]
    };
    
    $scope.SetupBusiness = function(business){
        $scope.Business = business;
        $scope.Edit = true;
        
        if(business.BusinessImage != ''){
            document.getElementById("BusinessUpload").style.display = "none";
            document.getElementById("BusinessImage").style.display = "block";
        }
        else{
            document.getElementById("BusinessUpload").style.display = "block";
            document.getElementById("BusinessImage").style.display = "none";
        }
        
        angular.element("#businessModal").modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }
    
    $scope.CloseModal = function(){
        document.getElementById("BusinessFile").value = "";
        angular.element("#businessModal").modal("hide");
    }
    
    $scope.DeleteBusiness = function(business){
        $scope.Business = business;
        
        if($scope.Business.IsPublic)            
            $scope.DeleteMessage = "Cannot delete a business that is public.  Please update the business first and then delete it.";
        else
            $scope.DeleteMessage = "Are you sure you want to delete business " + business.BusinessName + "?";
        
        $scope.Edit = false;
        angular.element("#businessModal").modal('show');
    }
    
    //pagination    
    $scope.SelectPage = function(newPage){
        $scope.SelectedPage = newPage;
    }
    
    //image 
    $scope.ProfileImage = function (element) {
        document.getElementById("BusinessFile").classList.remove('cancel-image');

        let reader = new FileReader();
        reader.onload = function (event) {
            image.setAttribute('crossOrigin', 'anonymous');
            image.src = event.target.result;
            setupCanvas("BusinessCanvas", 350, 150);
            image.onload = onImageLoad.bind(this);
        }
        reader.readAsDataURL(element.files[0]);

        document.getElementById("BusinessSlider").oninput = updateScale.bind(this);
        document.getElementById("BusinessUpload").style.display = "block";
        document.getElementById("BusinessImage").style.display = "none";
    }
    
    $scope.cancelFile = function () {
        document.getElementById("BusinessFile").value = "";
        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
        document.getElementById("BusinessCancel").classList.add('cancel-image');
    }
    
    $scope.SaveProfileImage = function () {
        let vData = null;

        if (ctx.canvas == '') {
        }
        else {
            let temp_canvas = document.createElement("canvas");
            let temp_ctx = temp_canvas.getContext('2d');
            temp_canvas.width = 250;
            temp_canvas.height = 250;
            temp_ctx.drawImage(ctx.canvas, 20, 20, 200, 200, 0, 0, 250, 250);
            vData = temp_canvas.toDataURL();
            vData = vData.replace('data:image/png;base64,', '');
        }

        $http({
//            method: "post",
//            url: dataUrl + "Admin/SaveProfileImage",
//            datatype: "json",
//            data: JSON.stringify({ newImage: vData })
//        }).then(function (response) {
//            let message = response.data;
//            if (message == '')
//                message = "Image saved successfully!";
//
//            alert(message);
//            HideModal();
//            $scope.GetProfile();
        });
    }
});