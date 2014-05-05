module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    concat : {
      dist : {
        src : ["js/*.js"],
        dest : 'rss.js'
      }
    },
    connect: {
      server : { 
        options : {
          port:5554,
          base: './',
          hostname:'*',
          keepalive : true,
          open : {
            target:'http://localhost:5554'
          }
      }
    }
  },
    ngconstant : {
      options : {
        name:"config",
        dest : 'js/config.js'
      },
      dev : {
        constants : {
          env : "http://localhost:5555/"
        }
      },
      prod : {
         constants : {
          env : "http://api.aggrss.eu/"
        }
      }

    }
});

  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-connect');
  grunt.loadNpmTasks('grunt-ng-constant');
  // Default task(s).
  grunt.registerTask('build', ['ngconstant:dev','concat']);
  grunt.registerTask('run', ['connect']);
  grunt.registerTask('go', ['ngconstant:dev','concat','connect']);


};