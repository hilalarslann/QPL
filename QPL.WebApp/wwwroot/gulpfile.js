const { src, watch, dest } = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const webpackStream = require('webpack-stream');
const webpack = require('webpack')
const log = require('fancy-log');

function javascript(cb) {
    src('./assets/js/main.js')
    .pipe(
        webpackStream({
        mode: 'development',
        plugins: [
            new webpack.ProvidePlugin({
              '$': 'jquery',
              'DevExpress': 'devextreme/bundles/modules/core',
              'ExcelJS': 'exceljs'
            })
        ]      
      })
    )
    .on('error', log)
    .pipe(dest('dist'));

    cb();
}

function css(cb) {
    src('./assets/sass/main.scss')
    .pipe(sourcemaps.init())
        .pipe(sass({
            includePaths: ['node_modules']
        }).on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(dest('dist'));
        
    cb();
}

exports.build = function(cb) {
    css(cb);
    javascript(cb);
};

exports.default = function() {
    watch('assets/sass/**/*.scss', { ignoreInitial: false }, css);
    watch('assets/js/**/*.js', { ignoreInitial: false }, javascript);
};
