

//var grunt = require('grunt');
//var config = grunt.config.data.connect.use_socket_io;
//var socketio = config.socketio;

//socketio.sockets.on('connection', function (socket) {
//    socket.emit('test', { message: "Hello!" });
//});

module.exports = function(grunt) {
  // Project configuration.
  const config = grunt.file.readJSON('config.json');
  grunt.log.writeln('\n\n\n\n\n\n\n\n\n\n');
  /**
   * setting task concat, uglyfi
   */
  const jsSrc  =  ()=>{
    return config.js['src'].map(element => {  
      !grunt.file.isFile(config.js['path'] + element['path'])?
        grunt.log.error()&&
        grunt.log.error(config.js['path'] + element['path']+" is not found!")
      :"";
      return config.js['path'] + element['path'];
    });
  };
  const js = {
    run : config.js["run"],
    minifile : !config.js["minifile"],
    src: jsSrc(),
    dest: config.js['dest'],
    path: config.js['path']
  };
  /**
   * setting task scss
   */
  const scssSrc = ()=>{
    return config.scss['src'].map(element => {
      return config.scss['path'] + element['path'];
    });
  }; 
  const scss = {
    run : config.scss["run"],
    src: scssSrc(),
    dest: config.scss['dest'],
    path: config.scss['path']
  };
  /**
   * setting task pug
   */
  const pugSrc = ()=>{
    var a = config.pug['src'].map(element=>{
      if(element['run']){   
        if(grunt.file.isFile(config.pug['path'] + element['path'])){
          var pug_file = {
            cwd: config.pug['path'],
            nonull: true,
            expand: true,
            filter: 'isFile',
            src: element['path'],
            dest:  element['dest'],
            ext: ".html"
          };
          return pug_file;
        }else
        {
          return config.pug['path'] + element['path'];
        }
      }
    });
    for(var i = 0; i<a.length; i++){
      !a[i]?a.splice(i, 1)&&i--:"";
    }
    return a;
  }; 
  const pug = {
    run : config.pug["run"],
    src: pugSrc(),
    path: config.pug['path']
  };
  /**
   * setting task copy
   */
  const copy_files = ()=>{
    var a = config.copy['files'].map(element=>{
      if(element['run']){   
        if(grunt.file.isFile(config.copy['src'] + element['src'])){
          var copy_file = {
            cwd: config.copy['src'],
            nonull: true,
            expand: true,
            filter: 'isFile',
            src: element['src'],
            dest:  element['dest'],
          };
          return copy_file;
        }else
        {
          return config.copy['src'] + element['src'];
        }
      }
    });
    for(var i = 0; i<a.length; i++){
      !a[i]?a.splice(i, 1)&&i--:"";
    }
    return a;
  };
  const copys = copy_files();
  /**
   * setting task watch
   */
  const tasks = ()=>{
    var tasks = [];
    js.run? tasks.push("uglify"): "";
    scss.run? tasks.push("sass"): "";
    pug.run? tasks.push("pug"): "";
    return tasks;
  };
  const file = ()=>{
    var file = ['Gruntfile.js','config.json'];
    js.run? file.push( js.path + "*.js"): "";
    scss.run? file.push( scss.path + "**/*.scss"): "";
    pug.run? file.push( pug.path + "**/*.pug"): "";
    return file;
  };
  const watch = {
    tasks: tasks(),
    file: file()
  };
  /////////////////////////////
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    uglify: {
      options:{
          banner: "var App ={checkLickPage: true}; $(document).ready(function(){",
        footer : "\n});",
        separator: ';',
        beautify: js.minifile,
        sourceMap: true,
      },
      build: {
        src: js.src,
        dest: js.dest
      }
    }, 

    sass: {
      options:{
        update: true,
      },
      dist: {
        src: scss.src,
        dest: scss.dest,
      }
    },

    pug: {
      compile: {
        options: {
          pretty: true,  
          data: {
            debug: true
          }
        },  
        files: pug.src,
      }
    },

    copy: {
      main: {
        files: copys
      },
      options:{
        nonull: true,
      }
    },

    //connect: {
    //    server: {
    //        options: {
    //            port: 8000,
    //            hostname: '*',
    //            base: 'www-root',
    //            socketio: true,
    //            onCreateServer: function(server, connect, options) {
    //                var io = require('socket.io').listen(server);
    //                io.sockets.on('connection', function(socket) {
    //                    // do something with socket
    //                });
    //            }
    //        }
    //    }
    //},

    watch: {
      scripts: {
        files: watch.file,
        tasks: watch.tasks,
        options: {
          spawn: false,
          interrupt: true,
          reload: true,
        },
      }
    },
  });

  // Load the plugin that provides the "uglify" task.
  grunt.loadNpmTasks('grunt-contrib-uglify');
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-sass');
  grunt.loadNpmTasks('grunt-contrib-pug');
  grunt.loadNpmTasks('grunt-contrib-connect');
  //grunt.loadNpmTasks('grunt-connect-socket.io');
  // Default task(s).
  grunt.registerTask('default', ['uglify', 'sass', 'pug', 'watch']);
};
