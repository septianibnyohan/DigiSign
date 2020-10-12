/*!
 * Signer
 * Version 1.0 - built Sat, Oct 6th 2018, 01:12 pm
 * https://simcycreative.com
 * Simcy Creative - <hello@simcycreative.com>
 * Private License
 */

var pdfDoc = null,
    pageNum = 1,
    pageRendering = false,
    pageNumPending = null,
    scale = 1.1,
    password = null;
    canvas = document.getElementById('document-viewer'),
    ctx = canvas.getContext('2d');

/**
 * Get page info from document, resize canvas accordingly, and render page.
 * @param num Page number.
 */
function renderPage(num) {
    $(".document-load").show();
    $(".signer-element").hide();
    rotationDegree = getActualRotation(num);
    pageRendering = true;
    // Using promise to fetch the page
    pdfDoc.getPage(num).then(function(page) {
        if (rotationDegree > 0) {
            var viewport = page.getViewport($(".document-map").width() / page.getViewport(scale).width, parseInt(rotationDegree + page.rotate) );
        }else{
            var viewport = page.getViewport($(".document-map").width() / page.getViewport(scale).width);
        }
        canvas.height = viewport.height;
        canvas.width = viewport.width;
        var renderContext = {
            canvasContext: ctx,
            viewport: viewport
        };
        var renderTask = page.render(renderContext);

        // Wait for rendering to finish
        renderTask.promise.then(function() {
          $(".document-load").hide();
          showTemplateFields();
          acceptRequest()
          $("[page="+pageNum+"]").show();
          modules.original = $('canvas').getCanvasImage("image/png");
          if (pageNum == pdfDoc.numPages) {
              $("#next").addClass("disabled");
          } else {
              $("#next").removeClass("disabled");
          }

          if (pageNum == 1) {
              $("#prev").addClass("disabled");
          } else {
              $("#prev").removeClass("disabled");
          }

          pageRendering = false;
          if (pageNumPending !== null) {
              // New page rendering is pending
              renderPage(pageNumPending);
              pageNumPending = null;
          }

          //ctx.globalAlpha = 0.3;
          //// setup text for filling
          //ctx.font = "72px Comic Sans MS";
          //ctx.fillStyle = "red";
          //// get the metrics with font settings
          //var metrics = ctx.measureText(classification_name);
          //var width = metrics.width;
          //// height is font size
          //var height = 72;

          //// change the origin coordinate to the middle of the context
          //ctx.translate(canvas.width / 2, canvas.height / 2);
          //// rotate the context (so it's rotated around its center)
          //ctx.rotate(-Math.atan(canvas.height / canvas.width));
          //// as the origin is now at the center, just need to center the text
          //ctx.fillText(classification_name, -width / 2, height / 2);

          if (typeof classification_name !== 'undefined') {
            var rotate = -Math.atan(canvas.height / canvas.width);
            //var rotate =0;
            var fontsize = Math.ceil(canvas.width / 15);
            drawText(classification_name, canvas.width / 2, (canvas.height / 2), fontsize, 'Comic Sans MS', rotate);
            ctx.font = "72px Comic Sans MS";
            var metrics = ctx.measureText("Viewed By " + employeeName);
            var width = metrics.width;
            //drawText2("Viewed By " + employeeName, canvas.width / 2, canvas.height / 2, fontsize/2, 'Comic Sans MS', rotate);

            drawRect(rotate, fontsize);
          } else {
            var rotate = 0;
            //var rotate = -Math.atan(canvas.height / canvas.width);
            //drawText(classification_name, canvas.width / 2, (canvas.height / 2) - 50, 72, 'Comic Sans MS', rotate);
            ctx.font = "18px Comic Sans MS";
            ctx.fillStyle = "red";
            var metrics = ctx.measureText("Viewed By " + employeeName);
            var width = metrics.width;
            //ctx.fillText("Viewed By " + employeeName, (canvas.width - width)/2, 36);
            //drawText2("Viewed By " + employeeName, (canvas.width - width) / 2, 18, 18, 'Comic Sans MS', rotate);

            //drawRect(rotate);
          }
          //var doc_width = canvas.width;
          //var doc_height = canvas.height;

          //var rotate = -1 * (Math.atan2(doc_height, doc_width)) * 180 / Math.PI;
          //var r = Math.sqrt(doc_width * doc_width + doc_height * doc_height);

          //$('#bg-text').css('-webkit-transform', 'rotate(' + rotate + 'deg)');
          //$('#bg-text').css('width', r)
          //$('#background').css('padding-top');
        });
    });

    

    // Update page counters
    $("#page_num").text(num);
    pageNum = num;

}

function drawRect(rotate,fontsize) {
  ctx.save();

  var doc_width = canvas.width;
  var doc_height = canvas.height;
  var r = Math.sqrt(doc_width * doc_width + doc_height * doc_height);

  ctx.globalAlpha = 0.3;
  ctx.globalCompositeOperation = "destination-over";
  ctx.fillStyle = "#00FFFF";
  //ctx.fillRect(0, 0, canvas.width, canvas.height);//for white background
  ctx.globalCompositeOperation = "source-over";
  ctx.lineWidth = 2;
  ctx.strokeStyle = "#FF0000";
  ctx.translate(canvas.width / 2, canvas.height / 2);
  ctx.rotate(rotate);
  var long = r - 300;
  var space = fontsize + fontsize / 2;
  var h = fontsize + fontsize / 2 + space;
  ctx.strokeRect(-long / 2, -space, long, h);//for white background
  ctx.restore();
}

function drawText(text, centerX, centerY, fontsize, fontface, rotate) {
  ctx.save();
  ctx.globalAlpha = 0.3;
  ctx.fillStyle = "red";
  ctx.font = fontsize + 'px ' + fontface;
  var metrics = ctx.measureText(text);
  var width = metrics.width;
  var height = fontsize;
  var space = 0;
  ctx.translate(centerX, centerY - fontsize - space);
  //ctx.textAlign = 'center';
  ctx.textBaseline = 'middle';
  ctx.rotate(rotate);
  ctx.fillText(text, -width / 2, height / 2);
  ctx.restore();
}

function drawText2(text, centerX, centerY, fontsize, fontface, rotate) {
  ctx.save();
  ctx.globalAlpha = 0.3;
  ctx.fillStyle = "red";
  ctx.font = fontsize + 'px ' + fontface;
  var metrics = ctx.measureText(text);
  var width = metrics.width;
  var height = fontsize;
  var space = 0;
  ctx.translate(centerX, centerY + fontsize + space);
  //ctx.textAlign = 'center';
  ctx.textBaseline = 'middle';
  ctx.rotate(rotate);
  ctx.fillText(text, -width / 2, height / 2);
  ctx.restore();
}

function jumpPage(num) {
  $(".document-load").show();
  $(".signer-element").hide();
  rotationDegree = getActualRotation(num);
  pageRendering = true;
  // Using promise to fetch the page
  pdfDoc.getPage(num).then(function (page) {
    if (rotationDegree > 0) {
      var viewport = page.getViewport($(".document-map").width() / page.getViewport(scale).width, parseInt(rotationDegree + page.rotate));
    } else {
      var viewport = page.getViewport($(".document-map").width() / page.getViewport(scale).width);
    }
    canvas.height = viewport.height;
    canvas.width = viewport.width;
    var renderContext = {
      canvasContext: ctx,
      viewport: viewport
    };
    var renderTask = page.render(renderContext);

    // Wait for rendering to finish
    renderTask.promise.then(function () {
      $(".document-load").hide();
      showTemplateFields();
      $("[page=" + pageNum + "]").show();
      modules.original = $('canvas').getCanvasImage("image/png");
      if (pageNum == pdfDoc.numPages) {
        $("#next").addClass("disabled");
      } else {
        $("#next").removeClass("disabled");
      }

      if (pageNum == 1) {
        $("#prev").addClass("disabled");
      } else {
        $("#prev").removeClass("disabled");
      }

      pageRendering = false;
      if (pageNumPending !== null) {
        // New page rendering is pending
        renderPage(pageNumPending);
        pageNumPending = null;
      }
    });
  });

  // Update page counters
  $("#page_num").text(num);
  pageNum = num;

}



/**
 * If another page rendering in progress, waits until the rendering is
 * finised. Otherwise, executes rendering immediately.
 */
function queueRenderPage(num) {
  if (pageRendering) {
    pageNumPending = num;
  } else {
    renderPage(num);
  }
}

/**
 * Displays previous page.
 */
function onPrevPage() {
  if (pageNum <= 1) {
    return;
  }
  pageNum--;
  queueRenderPage(pageNum);
}
document.getElementById('prev').addEventListener('click', onPrevPage);

/**
 * Displays next page.
 */
function onNextPage() {
  if (pageNum >= pdfDoc.numPages) {
    return;
  }
  pageNum++;
  queueRenderPage(pageNum);
}
document.getElementById('next').addEventListener('click', onNextPage);


/**
 * Asynchronously downloads PDF.
 */
openDocument(url, password);

function openDocument(url, password) {

    PDFJS.getDocument({
        url: url,
        password: password
    }).then(function(pdfDoc_) {
        pdfDoc = pdfDoc_;
        document.getElementById('page_count').textContent = pdfDoc.numPages;

        // Initial/first page rendering
        renderPage(pageNum);

        if (pdfDoc.numPages == 1) {
            $("#next, #prev").addClass("disabled");
        }

        $("#unlockFile").modal("hide");
        $(".document-password-error").hide();
    }).catch(function(error) {
        if (error.name == 'PasswordException') {
            $("input[name=docpassword]").val('');
            $("#unlockFile").modal({  show: true, backdrop: 'static', keyboard: false  });
            if (error.code == 2) {
                $(".document-password-error").show();
                $(".password-error").text(error.message);
            }
        } else {
            $(".document-error").find(".error-message").text(error.message);
            $(".document-load").hide();
            $(".document-error").show();
        }
    });

}

/*
 * Render the page again when browser is resized
 */
// $(document).resize(function(event, ui) {
//   // renderPage(pageNum);
// });

/*
 * Zoom in and Zoom Out
 */
$(".btn-zoom").click(function(){
    if($(this).attr("zoom") === "plus"){
        scale = scale - 0.1;
    }else{
        if (scale > 0) {
            scale = scale + 0.1;
        }
    }

    if (scale == 1) {
        $("#document-viewer").css("max-width", "100%");
    }else{
        $("#document-viewer").width("auto");
    }

    renderPage(pageNum);
});


/*
 * Tools responsiveness
 */
$('.signer-more-tools, .signer-header-tool-holder').slimscroll({
    height: '70px',
    width: '100%',
    railOpacity: '0',
    size: "1px",
    color: 'rgba(0, 0, 0, 0)',
    axis: 'x'
});


/*
 * unlock a protected file
 */
 $(".unlock-file").submit(function(event){
    event.preventDefault();
    openDocument(url, $("input[name=docpassword]").val());
 });


/*
 * re render page on resize
 */
 // $( window ).resize(function(){
 //    width = $( window ).width();
 //    if (width % 5 === 0) {
 //        renderPage(pageNum);
 //    }
 // });