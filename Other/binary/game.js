function $(id) {
	return document.getElementById(id);
}

var map01 = new Array();
map01[0] = new Array(new Array(1,1,0,0,'floor'), new Array(1,0,1,0,'floor'), new Array(1,0,0,0,'floor'), new Array(1,0,1,0,'floor'), new Array(1,0,0,1,'floor'));
map01[1] = new Array(new Array(0,1,0,1,'floor'), new Array(1,1,0,1,'floor'), new Array(0,1,0,1,'floor'), new Array(1,1,0,1,'floor'), new Array(0,1,1,1,'floor'));
map01[2] = new Array(new Array(0,1,0,1,'floor'), new Array(0,1,1,1,'floor'), new Array(0,1,0,1,'floor'), new Array(0,1,0,1,'floor'), new Array(1,1,0,2,'floor'));
map01[3] = new Array(new Array(0,1,0,0,'floor'), new Array(1,0,1,0,'floor'), new Array(0,0,0,1,'floor'), new Array(0,1,1,1,'floor'), new Array(0,1,0,1,'floor'));
map01[4] = new Array(new Array(0,1,1,1,'floor'), new Array(1,1,1,1,'floor'), new Array(0,1,1,0,'floor'), new Array(1,0,1,0,'floor'), new Array(0,0,1,1,'floor'));

function generateMapMenu() {
	var buttons = "<div class='maps main_content'>";
	buttons += '<button type="button" id="MAP01" onclick="setMaze(map01)" class="btn btn-default btn-lg btn-block">Play MAP 01</button>';
	buttons += '<button type="button" id="MAP02" class="btn btn-default btn-lg btn-block">Play MAP 02</button>';
	buttons += '<button type="button" id="MAP03" class="btn btn-default btn-lg btn-block">Play MAP 03</button>';
	buttons += '</div>';
	return buttons;
}

function setMaze(map) {
	$('fullContent').innerHTML = "<div class='main_content'>" + generateMap(map) + "</div>";
	buildMazeWall(map);
}


function generatepix() {
	var kitten = "<div style='width:100px; height:100px; background-color:red'></div>";
	return kitten;
}

$('playButton').onclick = function(){
	$('fullContent').innerHTML = generateMapMenu();
}


function generateMap(map) {
	var table ='<table class="maze">';
	for(var i = 0; i < map.length; i++) {
		table += '<tr>';
		for(var j = 0; j < map[i].length; j++) {
			table +='<td id="unit_'+i+'_'+j+'"></td>';
		}	
		table += '</tr>';
	}
	table += '</table>';
	return table;
}

function buildMazeWall(map) {
	for (var i = 0; i < map.length; i++){
		for (var j = 0; j < map[i].length; j++) {
			if(map[i][j][0] == 1){
				$('unit_'+i+'_'+j).style.borderTop = "2px solid black"; 
			}
			if(map[i][j][1] == 1){
				$('unit_'+i+'_'+j).style.borderLeft = "2px solid black"; 
			}
			if(map[i][j][2] == 1){
				$('unit_'+i+'_'+j).style.borderBottom = "2px solid black"; 
			}
			if(map[i][j][3] == 1){
				$('unit_'+i+'_'+j).style.borderRight = "2px solid black"; 
			}	
		}
	}
}

// 25px*25px 1 egység
function generateTable(size) {
	var table = "<table>";
	for(var i = 0; i<size; i++) {
		table += '<tr>';
		for(var j = 0; j<size; j++){
			table += '<td>' + '0' + '</td>';
		}
		table += '</tr>';
	}
	table += '</table>';
	return table;
}