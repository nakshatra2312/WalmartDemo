
var WalmartDemoApp = angular.module("WalmartDemoApp", ["ngRoute", "ngResource"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: CoursesCtrl, templateUrl: 'courselist.html' }).
            when('/coursecrud', { controller: CoursesCrudCtrl, templateUrl: 'coursecrud.html' }).
            when('/coursesearch', { controller: SearchCtrl, templateUrl: 'coursesearch.html' }).
            when('/courseadd', { controller: AddCtrl, templateUrl: 'courseadd.html' }).
            when('/listcourses', { controller: ListCtrl, templateUrl: 'listcourses.html' }).
            when('/courseedit', { controller: EditCtrl, templateUrl: 'courseedit.html' }).
            when('/courseedit/:editId', { controller: EditIdCtrl, templateUrl: 'courseeditid.html' }).
            when('/:userId', { controller: FavCtrl, templateUrl: 'favcourses.html' }).
            otherwise({ redirectTo: '/index.html' });
    });



WalmartDemoApp.factory('CourseTable', function ($resource) {
    return $resource('/api/Courses/:id', { id: '@id' }, { update: { method: 'PUT' } });
});


var CoursesCtrl = function ($scope, $location, CourseTable) {
    $scope.courses = CourseTable.query();
    $scope.user = "username";
    

    $scope.login = function () {
        debugger;
        $scope.welcome = "Welcome " + $scope.user + ".Click Yes to perform CRUD Operations or No to add favorites!";
    };
};

var CoursesCrudCtrl = function ($scope, $location, CourseTable) {

    $scope.courses = CourseTable.query();
};
var AddCtrl = function ($scope, $location, CourseTable) {
    
    $scope.save = function () {
        CourseTable.save($scope.course, function () {
            $location.path('/coursecrud');
        });
    };
};

var SearchCtrl = function ($scope, $location, CourseTable) {
    $scope.search = function () {
        debugger;
        $scope.courses = CourseTable.query({ id: $scope.query });
    };

    $scope.reset = function () {
        $scope.query = null;
        $scope.courses = CourseTable.query({ id: "!@#$%%^&&*" });
    };
    $scope.back = function () {
        $scope.query = null;
        $location.path('/coursecrud');
    };
};

var ListCtrl = function ($scope, $location, CourseTable) {

    $scope.courses = CourseTable.query();
    $scope.back = function () {
        $location.path('/coursecrud');
    };
};

var EditCtrl = function ($scope, $location, CourseTable) {
 
    $scope.search = function () {
        debugger;
        courseedit.Id = "tatat";
        $scope.courseedit = CourseTable.get({ id: $scope.query });
    };

    $scope.reset = function () {
        $scope.query = null;
        $scope.courses = CourseTable.query({ id: "!@#$%%^&&*"});
    };
    $scope.back = function () {
        $scope.query = null;
        $location.path('/coursecrud');
    };

    $scope.update = function () {
       
    };
};

var EditIdCtrl = function ($scope, $location, CourseTable, $routeParams) {

    var ids = $routeParams.editId;

    $scope.cobjs = CourseTable.query({ id: ids });

    $scope.edit = function () {
        debugger;

        var response = CourseTable.update({ id: ids }, this.cobj, function () {
            $location.path('/coursecrud');
        });

    };
};

var FavCtrl = function ($scope, $location, CourseTable, $routeParams) {

    var user = $routeParams.userId;
    $scope.courses = CourseTable.query();

    function isNormalInteger(str) {
        var n = Math.floor(Number(str));
        return n !== Infinity && String(n) === str && n >= 0;
    }
    var favlist = [];
    var lengthlist = [];
    $scope.favsofar = "";
    $scope.addfav = function (obj) {
        debugger;
        var id = obj.Id
        var length = obj.Length;
        if (!favlist.includes(id)) {
            favlist.push(id);
            $scope.favsofar = $scope.favsofar + id +","
            lengthlist.push(length);
        }
        
    };

    $scope.done = function () {
        debugger;
        var credit=0;
        for (i = 0; i < favlist.length; i++)
        {
            credit = +credit + +lengthlist[i];
        }

        $scope.message = user + " has signed up for " + favlist.length + " courses with "+ credit +" credits."

    };




};







