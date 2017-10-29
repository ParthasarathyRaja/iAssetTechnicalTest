var weatherapp = angular.module('myapp', ['ngRoute']);

weatherapp.config(function ($provide) {
    $provide.provider('ClimateService', function ($http) {
        this.$get = function () {
            var factory = {};

            factory.getClimate = function (pcity, pcountry) {
                //return $http({
                //    url: "Home/GetWeather",
                //    method: "GET",
                //    params: { city: pcity, country: pcountry }
                //});
                return "partha";
            }
            return factory;
        };
    });
});

//weatherapp.value("defaultInput", 5);

weatherapp.factory('ClimateService', function ($http) {
    var factory = {};

    factory.getClimate = function (pcity, pcountry) {
        //return $http({
        //    url: "Home/GetWeather",
        //    method: "GET",
        //    params: { city: pcity, country: pcountry }
        //});
        return "partha";
    }
    return factory;
});

weatherapp.service('WeatherService', function (ClimateService) {
    this.getWeather = function (city, country) {
        return ClimateService.getClimate(city, country);
    }
});

//weatherapp.controller('WeatherController', function ($scope, $http, WeatherService) {
weatherapp.controller('WeatherController', function ($scope) {
    $scope.city = "Chennai";
    $scope.country = "India";
    //$scope.result = WeatherService.getWeather($scope.city, $scope.country);

    $scope.getWeather = function () {
        alert("hi");
        $scope.result = "hai";//WeatherService.getWeather($scope.city, $scope.country);
    }
});

