
// Variable block
//-----------------------------------------------

var canvas;
var context;
var canvasWidth = 1590;
var canvasHeight = 800;
var linesArray = new Array;
var cutterArray = new Array;
var lastX = 0;
var lastY = 0;
var delay = false;
var cursorX;
var cursorY;
var eps = 1;
var t, r;
var res1;
var res2;
var rect;
var right;

var cuts = true;
var line = false;
var touch = false;


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

var colorCutter = Color.makeRGB(0, 0, 0);
var colorLine = Color.makeRGB(0, 0, 255);
var colorVisible = Color.makeRGB(255, 0, 0);
var colorOff = Color.makeRGB(255, 255, 255);


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
		touch = !touch;
		if(touch){
	    	addClick(mouseX, mouseY, touch);
		    canvas.style.cursor="crosshair";
		}else{
		    addClick(mouseX, mouseY, touch);
		    canvas.style.cursor="default";
			redraw();
		}
		mouseIsDown = true;
	}

	canvas.onmouseup = function(e){
	    mouseIsDown = false;
	}

	canvas.onmousemove = function(e){

		// else if (lines == true && touch == true)
		// {

		// }
	    cursorX = e.pageX;
	    cursorY = e.pageY;

		setInterval(checkCursor, 100);
		function checkCursor(){
		    document.getElementById('currentPosition').innerHTML = 'Mouse position: ' + cursorX + ", " + cursorY;
		}
	}

	$("#input-element-cutter-color").change(function($this) {
	    var c = $(this).val();

	    var r = parseInt(c.substring(1, 3), 16);
	    var g = parseInt(c.substring(3, 5), 16);
	    var b = parseInt(c.substring(5, 7), 16);
	    colorCutter = Color.makeRGB(r, g, b);
    });

    $("#input-element-line-color").change(function($this) {
	    var c = $(this).val();

	    var r = parseInt(c.substring(1, 3), 16);
	    var g = parseInt(c.substring(3, 5), 16);
	    var b = parseInt(c.substring(5, 7), 16);
	    colorLine = Color.makeRGB(r, g, b);
    });

    $("#input-element-visible-color").change(function($this) {
	    var c = $(this).val();

	    var r = parseInt(c.substring(1, 3), 16);
	    var g = parseInt(c.substring(3, 5), 16);
	    var b = parseInt(c.substring(5, 7), 16);
	    colorVisible = Color.makeRGB(r, g, b);
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

function addPoint()
{
	eps=document.getElementById("eps").value;
}

function lines()
{
	cuts = false;
	line = true;
}

function cutter()
{
	cuts = true;
	line = false;
}

function cut()
{
	rect = getMaxMin();
	for (var i = 0; i < (linesArray.length); i = i + 2)
	{
		pstart = linesArray[i];
		pend = linesArray[i+1];
		cutMiddlePoint(linesArray[i], linesArray[i+1], 1);
	}
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
	if (line)
	{
		linesArray.push([x, y]);
	}
	else if (cuts)
	{
		if (dragging)
			cutterArray.push([x, y]);
		else
		{
			var edge1 = [x, cutterArray[0][1]];
			var edge2 = [cutterArray[0][0], y];
			cutterArray.push(edge1, edge2, [x, y]);
			redraw();
			line = true;
			cuts = false;
		}
	}
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

function get_code(a, rect)
{
    var code = [0, 0, 0, 0];
    if (a[0] < rect[0])
        code[0] = 1;
    if (a[0] > rect[1])
        code[1] = 1;
    if (a[1] < rect[2])
        code[2] = 1;
    if (a[1] > rect[3])
        code[3] = 1;

	return code;
}

function getMaxMin()
{
	rect = [0, 0, 0, 0];
	rect[0] = Math.min(cutterArray[0][0], cutterArray[3][0]);
	rect[1] = Math.max(cutterArray[0][0], cutterArray[3][0]);
	rect[2] = Math.min(cutterArray[0][1], cutterArray[3][1]);
	rect[3] = Math.max(cutterArray[0][1], cutterArray[3][1]);

	return rect;
}

function cutMiddlePoint(p1, p2, d)
{
	var t1 = get_code(p1, rect);
	var t2 = get_code(p2, rect);
	var s1 = 0;
	var s2 = 0;

	//console.log(p1[0], p2[0]);
	for (var i = 0; i < 4; i++)
	{
		s1 = s1 + t1[i];
		s2 = s2 + t2[i];
	}
	
	var vis = 2;

	if (!s1 && !s2)
		vis = 1;
	else
	{	
		var p = 0;
		for (var i = 0; i < 4; i++)
			p += t1[i] & t2[i];
		if (p != 0)
			vis = 0;
	}

	//console.log(vis);

	if (vis == 1)
	{
		//alert("draw");
		drawVisibleLine(p1, p2);
		return 0;
	}
	else if (vis == 0)
		return 0;
	else
	{
		if (d > 2)
		{
			t1 = get_code(p1, rect);
			t2 = get_code(p2, rect);
			p = 0;
			for (var i = 0; i < 4; i++)
				p += t1[i] & t2[i];
			if (p == 0)
			{
				drawVisibleLine(p1, p2);
			}
			return 0;
		}
		else
		{
			var start = p1;
			if (s2 == 0)
			{
				t = p1;
				p1 = p2;
				p2 = t;
				//console.log(p1);
				cutMiddlePoint(p1, p2, d+1);
			}
			else
			{
				while (Math.sqrt((p1[0]-p2[0])*(p1[0]-p2[0]) + (p1[1]-p2[1])*(p1[1]-p2[1])) >= eps)
				{
					var pmid = [(p1[0] + p2[0])/2, (p1[1] + p2[1])/2];
					t = p1;
					p1 = pmid;
					t1 = get_code(p1, rect);
					t2 = get_code(p2, rect);
					p = 0;
					for (var i = 0; i < 4; i++)
						p += t1[i] & t2[i];
					if (p != 0)
					{
						p1 = t;
						p2 = pmid;
					}	
				}
				t = p1;
				p1 = p2;
				p2 = start;		
				// console.log("p1.x = ", p1[0]);
				// console.log("p2.x = ", p2[0]);	
				cutMiddlePoint(p1, p2, d+1);
			}
		}
	}
}

function drawVisibleLine(p1, p2)
{
	let oldStyle = context.strokeStyle;
	context.strokeStyle = colorVisible.cssRGB();
	context.beginPath();
	context.lineWidth = 1;
	context.lineJoin = context.lineCap = 'round';
	context.moveTo(p1[0], p1[1]);
	context.lineTo(p2[0], p2[1]);
	context.stroke();
	context.fillStyle = oldStyle;
}

function redraw()
{
	if (cuts)
	{
		let oldStyle = context.strokeStyle;
		context.strokeStyle = colorCutter.cssRGB();
		context.beginPath();
		context.lineWidth = 1;
		context.lineJoin = context.lineCap = 'round';
		context.moveTo(cutterArray[0][0], cutterArray[0][1]);
		context.lineTo(cutterArray[1][0], cutterArray[1][1]);
		context.moveTo(cutterArray[1][0], cutterArray[1][1]);
		context.lineTo(cutterArray[3][0], cutterArray[3][1]);
		context.moveTo(cutterArray[3][0], cutterArray[3][1]);
		context.lineTo(cutterArray[2][0], cutterArray[2][1]);
		context.moveTo(cutterArray[2][0], cutterArray[2][1]);
		context.lineTo(cutterArray[0][0], cutterArray[0][1]);
		context.stroke();
		context.fillStyle = oldStyle;
	}
	else
	{
		let oldStyle = context.strokeStyle;
		context.strokeStyle = colorLine.cssRGB();
		context.beginPath();
		context.lineWidth = 1;
		context.lineJoin = context.lineCap = 'round';
		var j = linesArray.length;
		context.moveTo(linesArray[j-2][0],linesArray[j-2][1]);
		context.lineTo(linesArray[j-1][0], linesArray[j-1][1]);
		context.stroke();
		context.fillStyle = oldStyle;
	}
}

//-----------------------------------------------