function ShareToSocialMedia(VideoDTO, Form) {
    window.fbAsyncInit = function (Id, Title, Description, ThumbnailURL, _Form) {
        if (typeof Id === "undefined") { Id = VideoDTO.Id; }
        if (typeof Title === "undefined") { Title = VideoDTO.Title; }
        if (typeof Description === "undefined") { Description = VideoDTO.Description; }
        if (typeof ThumbnailURL === "undefined") { ThumbnailURL = VideoDTO.ThumbnailURL; }
        if (typeof _Form === "undefined") { _Form = Form; }

        FB.init({
            appId: '1576894379240839',
            xfbml: true,
            version: 'v2.3'
        });
        FB.login(function (response) {
            FB.getLoginStatus(function (response) {
                if (response.status === 'connected') {
                    // the user is logged in and has authenticated your
                    // app, and response.authResponse supplies
                    // the user's ID, a valid access token, a signed
                    // request, and the time the access token 
                    // and signed request each expire
                    var uid = response.authResponse.userID;
                    var userAccessToken = response.authResponse.accessToken;

                    var params = {};
                    params['message'] = '';
                    params['name'] = Title;
                    params['description'] = Description;
                    params['link'] = 'http://vdovalley.com/videos/details/' + Id;
                    params['picture'] = ThumbnailURL;
                    params['caption'] = 'VdoValley.Com';
                    params['fb:explicitly_shared'] = true;
                    params['access_token'] = userAccessToken;
                    PublishToVdoValley();
                    /*
                    // share on Saba Ibrahim feed
                    FB.api('/me/feed', 'post', params, function (response) {
                        if (!response || response.error) {
                            PublishToVdoValley();
                        } else {
                            console.log('Published to SabaIbrahim');
                            PublishToVdoValley();
                        }
                    });
                    */

                    function PublishToVdoValley() {
                        // get access token for VdoValley
                        FB.api('/1379536292353915', { fields: 'access_token' }, function (resp) {
                            if (resp.access_token) {
                                params['access_token'] = resp.access_token;
                                FB.api('/1379536292353915/feed', 'post', params, function (response) {
                                    if (!response || response.error) {
                                        Form.find(".social-media").append("<span>VdoValley</span>");
                                        PublishToGoNawazGo();
                                    } else {
                                        console.log('Published to VdoValley');
                                        PublishToGoNawazGo();
                                    }
                                });
                            } else {
                                alert("Error retrieving page access token");
                            }
                        });
                    }
                    
                    function PublishToGoNawazGo() {
                        // get access token for Go Nawaz Go
                        FB.api('/295834287269305', { fields: 'access_token' }, function (resp) {
                            if (resp.access_token) {
                                params['access_token'] = resp.access_token;
                                FB.api('/295834287269305/feed', 'post', params, function (response) {
                                    if (!response || response.error) {
                                        Form.find(".social-media").append("<span>GoNawazGo</span>");
                                        PublishToMeriJang();
                                    } else {
                                        console.log('Published to Go Nawaz Go');
                                        PublishToMeriJang();
                                    }
                                });
                            } else {
                                alert("Error retrieving page access token");
                            }
                        });
                    }

                    function PublishToMeriJang() {
                        // get access token for Go Nawaz Go
                        FB.api('/1611990939043872', { fields: 'access_token' }, function (resp) {
                            if (resp.access_token) {
                                params['access_token'] = resp.access_token;
                                FB.api('/1611990939043872/feed', 'post', params, function (response) {
                                    if (!response || response.error) {
                                        Form.find(".social-media").append("<span>MeriJang</span>");
                                        PublishToKharaSach();
                                    } else {
                                        console.log('Published to Meri Jang');
                                        PublishToKharaSach();
                                    }
                                });
                            } else {
                                alert("Error retrieving page access token");
                            }
                        });
                    }

                    function PublishToKharaSach() {
                        // get access token for KharaSach
                        FB.api('/1649340585329385', { fields: 'access_token' }, function (resp) {
                            if (resp.access_token) {
                                params['access_token'] = resp.access_token;
                                FB.api('/1649340585329385/feed', 'post', params, function (response) {
                                    if (!response || response.error) {
                                        Form.find(".social-media").append("<span>KharaSach</span>");
                                        PublishToBolJasmeenKaySath();
                                    } else {
                                        console.log('Published to Khara Sach');
                                        PublishToBolJasmeenKaySath();
                                    }
                                });
                            } else {
                                alert("Error retrieving page access token");
                            }
                        });
                    }
                    
                    function PublishToBolJasmeenKaySath() {
                        // get access token for BolJasmeenKaySath
                        FB.api('/1610761492535208', { fields: 'access_token' }, function (resp) {
                            if (resp.access_token) {
                                params['access_token'] = resp.access_token;
                                FB.api('/1610761492535208/feed', 'post', params, function (response) {
                                    if (!response || response.error) {
                                        Form.find(".social-media").append("<span>BolJasmeenKaySath</span>");
                                    } else {
                                        console.log('Published to Bol Jasmeen Kay Sath');
                                    }
                                });
                            } else {
                                alert("Error retrieving page access token");
                            }
                        });
                    }
                                    
                } else if (response.status === 'not_authorized') {
                    // the user is logged in to Facebook, 
                    // but has not authenticated your app
                    alert("Not authorized");
                } else {
                    // not logged in
                }
            });
        }, {
            scope: 'publish_actions,manage_pages,publish_pages',
            return_scopes: true
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
}