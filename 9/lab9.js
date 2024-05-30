
// Variable block
//-----------------------------------------------

var canvas;
var context;
var canvasWidth = 1400;
var canvasHeight = 800;
var clickX = new Array();
var clickY = new Array();
var locked = new Array();
var lines = new Array();
var click = new Array();
var edge = [];
var paint = false;
var pointX = -1;
var pointY = -1;
var lastX = 0;
var lastY = 0;
var delay = false;
var cursorX;
var cursorY;
var currentColor = 0;
var minX = 2000;
var minY = 2000;
var maxX = 0;
var maxY = 0;


var cuts = true;
var line = false;

var Color = function() {
    this.r = this.g = this.b = 0;
};

Color.prototype.cssRGB = function() {
    return "rgb("+Math.round(this.r)+", "+Math.round(this.g)+", "+Math.round(this.b)+")";
};

Color.prototype.red = function() { return this.r; };
Color.prototype.green = function() { return this.g; };
Color.prototype.blue = function() { return this.b; };

Color.prototype.compare = function(other) {
	return (this.r == other.r) && (this.g == other.g) && (this.b == other.b);
}

Color.makeRGB = function() {
    var c = new Color();
    if(arguments.length < 3 || arguments.length > 4)
        throw new Error("error: 3 or 4 arguments");
    c.r = arguments[0];
    c.g = arguments[1];
    c.b = arguments[2];
    return c;
};

var colorOff = Color.makeRGB(255, 255, 255);
var colorOn = Color.makeRGB(0, 0, 0);
var colorBorder = Color.makeRGB(255, 0, 0);


//-----------------------------------------------
// Main block
//-----------------------------------------------

function prepareCanvas()
{
	canvas = document.getElementById("myCanvas");
 
	context = canvas.getContext("2d");

	context.imageSmoothingEnabled = false;

	init();

	var mouseIsDown = false;

	canvas.onmousedown = function(e){

		var rect = canvas.getBoundingClientRect();
	    var mouseX = e.clientX - rect.left;
		var mouseY = e.clientY - rect.top;

	    mouseIsDown = true;
	    addClick(mouseX, mouseY, false);
	}

	canvas.onmouseup = function(e){
	    mouseIsDown = false;
	}

	canvas.onmousemove = function(e){
	    cursorX = e.pageX;
	    cursorY = e.pageY;
	}

	setInterval(checkCursor, 100);
	function checkCursor(){
	    document.getElementById('currentPosition').innerHTML = 'Mouse position: ' + cursorX + ", " + cursorY;
	}

	$("#input-element-rect-color").change(function($this) {
	    var c = $(this).val();

	    var r = parseInt(c.substring(1, 3), 16);
	    var g = parseInt(c.substring(3, 5), 16);
	    var b = parseInt(c.substring(5, 7), 16);
	    colorOn = Color.makeRGB(r, g, b);
    });

    $("#input-element-background-color").change(function($this) {
	    var c = $(this).val();

	    var r = parseInt(c.substring(1, 3), 16);
	    var g = parseInt(c.substring(3, 5), 16);
	    var b = parseInt(c.substring(5, 7), 16);
	    colorOff = Color.makeRGB(r, g, b);
	    init();
	    redraw();
    });

}

function init()
{
	let oldStyle = context.fillStyle;
	context.fillStyle = colorOff.cssRGB();
	context.fillRect(0, 0, canvasWidth, canvasHeight);
	context.fillStyle = oldStyle;
}

//-----------------------------------------------
// Button block
//-----------------------------------------------

function cut()
{
	cutEdgeAlgorithm();
}

function delayCheck(checkboxElem)
{
	if (checkboxElem.checked)
		delay = true;
	else
		delay = false;
}

function circuit()
{
	if (!line)
	{
		var i = clickX.length;
		if (clickX[i-1] != pointX || clickY[i-1] != pointY)
		{
			addClick(pointX, pointY, false);
			pointX = -1;
			pointY = -1;
			redraw();
		}
		line = true;
	}
	else
	{
		lines.push([lines[0][0], lines[0][1]]);
		redraw();
	}
}

function addPoint()
{
	var x=document.getElementById("results").value;
	var y=document.getElementById("results2").value;

    results = parseFloat(x);
    if(results==null)
        results=0;
    results2 = parseFloat(y);
    if(results2==null)
        results2=10;
	addClick(results, results2, false);
}

function clearAll()
{
	document.location.reload(true);
}

//-----------------------------------------------
// Function block
//-----------------------------------------------

function addClick(x, y, dragging)
{
	if (!line)
	{
		checkForMinMax(x, y);
		click.push([x, y]);
		clickX.push(x);
		clickY.push(y);
		if (pointX == -1 && pointY == -1)
		{
			locked.push(1);
			pointX = x;
			pointY = y;
		}
		else
		{
			buildEdge();
			locked.push(0);
		}
		redraw();
	}
	else
	{
		lines.push([x, y]);
		redraw();
	}
}

function checkForMinMax(x, y)
{
	if (x < minX)
		minX = x;
	if (y < minY)
		minY = y;
	if (x < maxX)
		maxX = x;
	if (y > maxY)
		maxX = y;
}
function buildEdge()
{
	var i = clickX.length;
	edge.push([clickX[i-1], clickY[i-1], clickX[i-2], clickY[i-2]]);
	//alert(edge[0][0]);
}

function clearCanvas()
{
	context.clearRect(0, 0, canvasWidth, canvasHeight);
}

//-----------------------------------------------
// Draw block
//-----------------------------------------------

function colorAtIndex(imageData, index)
{
	let color = Color.makeRGB(imageData.data[index], imageData.data[index + 1], imageData.data[index + 2]);
	return color;
}

function sign(x)
{
    if (!x)
        return 0
    else
		return x / Math.abs(x);
}

function scalar(v1, v2)
{
	return v1[0] * v2[0] + v1[1] * v2[1];
}


function countSign(vo, vi, vn)
{
    var x1 = vi[0] - vo[0];
    var y1 = vi[1] - vo[1];

    var x2 = vn[0] - vi[0];
    var y2 = vn[1] - vi[1];

    var r = x1 * y2 - x2 * y1;
    return sign(r);
}

function checkForConvex()
{
    var flag = 1

    var v1 = [clickX[0], clickY[0]];
    var v2 = [clickX[1], clickY[1]];
    var v3 = [clickX[2], clickY[2]];

    var prev = countSign(v1,v2,v3);
    var curr;

    for (var i = 2; i < clickX.length - 1; i++)
    {
        if (!flag)
        {
            break;
        }
        v1 = [clickX[i-1], clickY[i-1]];
        v2 = [clickX[i], clickY[i]];
        v3 = [clickX[i+1], clickY[i+1]];

        curr = countSign(v1,v2,v3);

        if (curr != prev)
            flag = 0;
        prev = curr;
    }

    v1 = [clickX[clickX.length - 2], clickY[clickX.length - 2]];
    v2 = [clickX[0], clickY[0]];
    v3 = [clickX[1], clickY[1]];

    curr = countSign(v1, v2, v3);
    
    if (curr != prev)
    {
        flag = 0;
    }
	return flag * curr;
}

function findIntersections(lineOfPoly, border, check)
{
    var v1 = visiblePoint(lineOfPoly[0], border[0], border[1], check);
    var v2 = visiblePoint(lineOfPoly[1], border[0], border[1], check);
    if ((v1 && !v2) || (!v1 && v2))
    {
        var p1 = lineOfPoly[0];
        var p2 = lineOfPoly[1];

        var q1 = border[0];
        var q2 = border[1];

        var delta = (p2[0] - p1[0]) * (q1[1] - q2[1]) - (q1[0] - q2[0]) * (p2[1] - p1[1]);
        var delta_t = (q1[0] - p1[0]) * (q1[1] - q2[1]) - (q1[0] - q2[0]) * (q1[1] - p1[1]);

        var t = delta_t / delta;

        var I = [lineOfPoly[0][0] + (lineOfPoly[1][0] - lineOfPoly[0][0]) * t, lineOfPoly[0][1] + (lineOfPoly[1][1] - lineOfPoly[0][1]) * t];
        return I;
    }
    else
        return false;
}


function visiblePoint(point, border1, border2, check)
{
    var v = vector([point, border1], [border2, border1]);
    if (check * v < 0)
        return true;
    else
        return false;
}

function vector(v1, v2)
{
    x1 = v1[0][0] - v1[1][0];
    y1 = v1[0][1] - v1[1][1];

    x2 = v2[0][0] - v2[1][0];
    y2 = v2[0][1] - v2[1][1];

	return x1 * y2 - x2 * y1;
}

function redrawScreen()
{
	var check = checkForConvex();
	if (check)
	{
		var s = 0;
		var t = 0;
		var f = 0;
		for(var i = 0; i < click.length-1; i++)
		{
			var result = new Array();
	        for(var j = 0; j < lines.length; j++)
	        {
	        	if (j == 0)
					f = lines[j];
				else
				{
		            t = findIntersections([s, lines[j]], [click[i], click[i + 1]], check);
		            if(t)
		            {
		                result.push(t);
		            }
		        }

	            s = lines[j];

	            if(visiblePoint(s,  click[i], click[i + 1], check))
	            {
	                result.push(s);
	            }
	        }

	        if (result.length != 0)
	        {
            	t = findIntersections([s, f], [click[i], click[i + 1]], check);
            	if (t)
					result.push(t);
			}
	        lines = result;
	    }
	    if (lines.length == 0)
	    {
	        return false;
	    }
	    else
	    {
	    	draw();
	    }
	}
	else
	{
		alert("The figure is not convex. ");
	}
}

function draw()
{
	let oldStyle = context.strokeStyle;
	context.strokeStyle = colorBorder.cssRGB();
	context.beginPath();
	context.lineWidth = 1;
	context.lineJoin = context.lineCap = 'round';
	context.lineJoin = context.lineCap = 'round';
	for (var i = 0; i < lines.length-1; i++) {
		context.moveTo(lines[i][0], lines[i][1]);
		context.lineTo(lines[i+1][0], lines[i+1][1]);
	}
	context.lineTo(lines[0][0], lines[0][1]);
	context.stroke();
	context.fillStyle = oldStyle;
}

function cutEdgeAlgorithm()
{
	redrawScreen();
}

function delay()
{
	var delayInMilliseconds = 5000;
	setTimeout(function() {
		return;
	}, delayInMilliseconds);
	return;
}

function redraw()
{
	if (!line)
	{
		let oldStyle = context.strokeStyle;
		context.strokeStyle = colorOn.cssRGB();
		context.beginPath();
		context.lineWidth = 1;
		context.lineJoin = context.lineCap = 'round';
		context.lineJoin = context.lineCap = 'round';
		var i;
		var j = clickX.length;
		context.moveTo(clickX[j-2],clickY[j-2]);
		for (i = j-1; i < clickX.length; i++) {
			//context.fillRect(clickX[i], clickY[i], 2, 2);
			if (locked[i] == 1){
				context.moveTo(clickX[i], clickY[i]);
			}
			else{
				context.lineTo(clickX[i], clickY[i]);
			}
		}
		context.stroke();
		context.fillStyle = oldStyle;
	}
	else
	{
		let oldStyle = context.strokeStyle;
		context.strokeStyle = colorOn.cssRGB();
		context.beginPath();
		context.lineWidth = 1;
		context.lineJoin = context.lineCap = 'round';
		var j = lines.length;
		context.moveTo(lines[j-2][0], lines[j-2][1]);
		context.lineTo(lines[j-1][0], lines[j-1][1]);
		context.stroke();
		context.fillStyle = oldStyle;
	}
}

//-----------------------------------------------