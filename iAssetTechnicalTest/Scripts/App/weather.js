var mainApp = angular.module("mainApp", []);
var data = new Object();
var apiKey = "4acf9196cb8f4acc39233be4fb93788b";

//AngualrJs Dependency Injection : PROVIDER
mainApp.config(function ($provide) {
    $provide.provider('ClimateService', function () {
        this.$get = function () {
            var factory = {};

            factory.multiply = function (a, b) {
                var initInjector = angular.injector(['ng']);
                var $http = initInjector.get('$http');
                return $http.get("/Home/GetWeather", { params: { city: a, country: b } });
            }

            factory.getCities = function (a) {
                var initInjector = angular.injector(['ng']);
                var $http = initInjector.get('$http');
                $http.get("/Home/GetCitiesByCountry", { params: { country: a } })
                .then(function (success) {
                    debugger;
                    data = success.data;
                    return success.data;
                },
                function (error) {
                    debugger;
                    data = error.data;
                });
            }
            return factory;
        };
    });
});

//AngualrJs Dependency Injection : VALUE
//mainApp.value("defaultCity", "London");
//mainApp.value("defaultCountry", "uk");

//AngualrJs Dependency Injection : FACTORY
mainApp.factory('ClimateService', function () {
    var factory = {};

    factory.multiply = function (a, b) {
        //return a + b;
        var initInjector = angular.injector(['ng']);
        var $http = initInjector.get('$http');
        return $http.get("/Home/GetWeather", { params: { city: a, country: b } });
    }

    factory.getCities = function (a) {
        var initInjector = angular.injector(['ng']);
        var $http = initInjector.get('$http');
        $http.get("/Home/GetCitiesByCountry", { params: { country: a } })
        .then(function (success) {
            debugger;
            data = success.data;
            return success.data;
        },
        function (error) {
            debugger;
            data = error.data;
        });
    }
    return factory;
});

//AngualrJs Dependency Injection : SERVICE
mainApp.service('WeatherService', function (ClimateService) {
    this.square = function (a, b) {
        return ClimateService.multiply(a, b);
    }
    this.getCities = function (a) {
        return ClimateService.getCities(a);
    }
});

mainApp.controller('WeatherController', function ($scope, $http, WeatherService) {

    $scope.getWeather = function () {
        //$scope.result = CalcService.square($scope.city, $scope.country);
        debugger;
        $http.get("/Home/GetWeather", { params: { city: $scope.selectedCity.City, country: $scope.country } })
        .then(function (success) {
            debugger;
            data = success.data;
            if (data != "") {
                $scope.result = data;
            }
            else {
                //When the first web service does not give result, use the second service for data.
                //API Key - Create an account in openweathermap web site and get a key.
                $http.get("http://api.openweathermap.org/data/2.5/weather", { params: { q: $scope.selectedCity.City + "," + $scope.country, appid: apiKey } })
                    .then(function (success) {
                        debugger;
                        data = success.data;

                        $scope.location = success.data.name;
                        $scope.time = Date();
                        $scope.wind = "Speed: " + success.data.wind.speed + " m/s, Degree: " + success.data.wind.deg;
                        $scope.visibility = success.data.visibility;
                        $scope.sky = success.data.weather[0].description;
                        $scope.temparature = success.data.main.temp;
                        $scope.dewPoint = "-";
                        $scope.humidity = success.data.main.humidity + " %";
                        $scope.pressure = success.data.main.pressure + " hpa";
                        $scope.urlused = "http://api.openweathermap.org/data/2.5/weather?q=" + $scope.selectedCity.City + "," + $scope.country+"&appid=" + apiKey;
                    },
                    function (error) {
                        debugger;
                        data = error.data;
                        $scope.error = error.data;
                    });
                //
            }
        },
        function (error) {
            debugger;
            $scope.error = "Error fetching weather details for country: " + $scope.country + ", city: " + $scope.selectedCity.City;
        });

    }
    $scope.getCities = function () {
        debugger;
        console.log("get cities called");

        //Use dependency injection to fetch and populate data.
        //$scope.Citylist = CalcService.getCities($scope.country); 

        $http.get("/Home/GetCitiesByCountry", { params: { country: $scope.country } })
        .then(function (success) {
            debugger;
            data = success.data;
            $scope.Citylist = JSON.parse(data).NewDataSet.Table;
        },
        function (error) {
            debugger;
            $scope.error = "Error fetching cities details for " + $scope.country;
        });
    }
});