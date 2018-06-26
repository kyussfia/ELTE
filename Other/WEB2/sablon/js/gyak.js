/*function handler() {
	alert('ok');
}
document.onclick = handler();
*/
function $(id) {
return document.getElementById(id);
}
/*
string.match(regexp) email validate pl

*/

document.getElementById("form").onsubmit = function(e) {
var hibak = [];
	var nev = document.getElementById('nev');
	var kor = document.getElementById('kor');
	var erd = document.getElementById('erd');
	
	var  sex = $("sex");
	if(nev.value == "") {
	
	hibak.push("Nem töltötté");
	}
	
	for(var j = 0; j < sex.length; j++) {
	var checked = false;
		if(sex[j].checked == true) {
		checked = true;
		} else {
		checked =false;
		}
	}
	if(checked == false) {
		hibak.push('Add emg a nemed');
	}
	
	if(isNaN(parseInt(kor.value))) {
	hibak.push("Nem töltötté kort");
	}
	
	if(erd.value == 0) {
	hibak.push("válassz");
	}
	var html = '';
	if(hibak.length > 0) {
		for(var i=0; i < hibak.length; i++) {
			html += '<li>' + hibak[i] + '</li>';
		}
		$('hibak').innerHTML = html;
	}
	e.preventDefault();
}

/*$('kep').onmouseover = function() {
	$('kepes').innerHTML = "KÉÉÉP";
}
$('kep').onmouseout = function() {
	$('kepes').innerHTML = "";
}
*/
var kepek = $('kepek').childNodes;
//1, 3, 5
$('next').onclick = function() {
if($('most').getAttribute("src") == "img/1.jpg"){
	$('most').setAttribute("src","img/3.jpg");
}
}

$('prev').onclick =  function() {
$('most').setAttribute("src","img/5.jpg");
}


 function bela() {
   var input =document.getElementById('text');
   if(isNaN(parseInt(input.value))){
   alert("nem szam ");
   } else {
	 input.value = parseInt(input.value)+1
	 }
}

document.getElementById("lista").childNodes.onclick = function (event) {
	var elem = event.target;
	if(elozo === null) {
		elozo = elem;
	} else {
		var tmp = elem.innerHTML;
		elem.innerHTM = elozo.innerHTML;
		elozo.innerHTML = tmp;
		elozo = null;
	}
	


}

function startTime()
{
var today=new Date();
var h=today.getHours();
var m=today.getMinutes();
var s=today.getSeconds();
// add a zero in front of numbers<10
m=checkTime(m);
s=checkTime(s);
document.getElementById('txt').innerHTML=h+":"+m+":"+s;
t=setTimeout(function(){startTime()},500);
}

function checkTime(i)
{
if (i<10)
  {
  i="0" + i;
  }
return i;
}

$('szines').onclick = function(){
	var style = $('p').style;
	style.color = 'blue';
	style.backgroundColor = 'brown';
	style.borderStyle = 'solid';
	style.borderWidth = '1px';
	style.borderColor = 'Orange';
}
/*
e. preventDefault() letiltja az eredtgi funciokat
*/


/*
var fv = handler();
fv();
*/

//document.write()
//condole.log()