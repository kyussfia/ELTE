/*----------------------------------------------------------------------

	WEB-FEJLESZTÉS 2. JAVASCRIPT BEADANDÓ
	Készítette: Mikus Márk (CM6TSV)

----------------------------------------------------------------------*/

var gGameField = [];     // a játékos által adott állapotok
var gSolution  = [];     // a megoldást tartalmazó mátrix
var gRowLists  = [];     // a sorok mellett elhelyezkedő számlisták
var gColLists  = [];     // az oszlopok fölött elhelyezkedĹő számlisták
var gGameTimer = null;   // az órát frissitő időzítő
var gPlayTime  = 0;      // a játékidő (másodpercben)
var gSolved    = false;  // a játék meg van-e oldva vagy sem
var gPaused    = true;   // a játék meg van-e állítva vagy sem
var gComplete  = [];     // befejezett sorok és oszlopok
var gQuickMode = false;  // gyors mód állapota


//----------------------------------------------------------------------
//  Statikus mátrixok - Feladatok
//----------------------------------------------------------------------

			var gSmallGame = [ "0001000100", "0011101110", "0011101110", "0001000100", "0111011110", "0111011110", "0001000100", "1110111011", "0111111111", "0011111110" ];
			
/*var gSmallGame = [
		[0,0,0,1,0,0,0,1,0,0],
		[0,0,1,1,1,0,1,1,1,0],
		[0,0,1,1,1,0,1,1,1,0]
		[0,0,0,1,0,0,0,1,0,0]
		[0,1,1,1,0,1,1,1,1,0]
		
		[0,1,1,1,0,1,1,1,1,0]
		[0,0,0,1,0,0,0,1,0,0]
		[1,1,1,0,1,1,1,0,1,1]
		[0,1,1,1,1,1,1,1,1,1]
		[0,0,1,1,1,1,1,1,1,0]
		];
*/
		var gLargeGame = [ "000001100111100", "000011110101110", "000011110100110", "000001101010100", "000000010111100", "000110001000010", "101110000111010", "011100000100010", "001111000100010", "001110100111100", "101110001111100", "111100011111110", "011000011101111", "010000011100111", "011000111000001" ];

/*var gLargeGame = [
		[0,0,0,0,0,1,1,0,0,1,1,1,1,0,0]
		[0,0,0,0,1,1,1,1,0,1,0,1,1,1,0]
		[0,0,0,0,1,1,1,1,0,1,0,0,1,1,0]
		[0,0,0,0,0,1,1,0,1,0,1,0,1,0,0]
		[0,0,0,0,0,0,0,1,0,1,1,1,1,0,0]
	
		[0,0,0,1,1,0,0,0,1,0,0,0,0,1,0]
		[1,0,1,1,1,0,0,0,0,1,1,1,0,1,0]
		[0,1,1,1,0,0,0,0,0,1,0,0,0,1,0]
		[0,0,1,1,1,1,0,0,0,1,0,0,0,1,0]
		[0,0,1,1,1,0,1,0,0,1,1,1,1,0,0]
		
		[1,0,1,1,1,0,0,0,1,1,1,1,1,0,0]
		[1,1,1,1,0,0,0,1,1,1,1,1,1,1,0]
		[0,1,1,0,0,0,0,1,1,1,0,1,1,1,1]
		[0,1,0,0,0,0,0,1,1,1,0,0,1,1,1]
		[0,1,1,0,0,0,1,1,1,0,0,0,0,0,1]
		];*/


/**
 *  $
 * 
 *  A függvény megadja az adott azonosítoval rendelkező objektumot.
 * 
 *  
**/
function $(id)
{
	return document.getElementById(id);
}


/**
 *  getLongestListLength
 * 
 *  A függvény megadja a megadott tömböket tartalmazó tömb elemei
 *  közötti leghosszabb tömb hosszát.
 * 
 *  @param list - tömböket tartalmazó tömb
**/
function getLongestListLength(list)
{
	var max = list[0].length;
	for (var i = 1; i < list.length; i++) {
		var len = list[i].length;
		if (max < len) {
			max = len;
		}
	}
	
	return max;
}


/**
 *  pad00
 *
 * A függvény egy tetszőleges számból, alakít ki egy olyan számot amely,
 *	legalább 2 számjegyből áll.(Egyjegyűekből csinál kétjegyűt)
 *	(órához kell)
 *  
 *  @param n - a formázandó szám
**/
function pad00(n){
	if (n < 10) {
		return "0" + n;
	}else{
		return n;
	}
} 


/**
 *  areListsEqual
 * 
 *  A függvény megvizsgálja, hogy két megadott lista (tömb)
 *  megegyezik-e elemről elemre.
 * 
 *  @param a - az első tömb
 *  @param b - a második tömb
**/
function areListsEqual(a, b)
{
	if (a.length != b.length) return false;
	var i = 0;
	while (i < a.length) {
		if (a[i] != b[i]) {
			return false;
		}
		i++;
	}
	return true;
}


/**
 *  addElemIfNotExists
 * 
 *  Az eljárás hozzáad egy elemet a megadott tömbhöz, ha az még nincs
 *  benne.
 * 
 *  @param array - a tömb, amihez hozzá akarjuk adni az elemet
 *  @param elem  - az elem, amit hozzá akarunk adni
**/
function addElemIfNotExists(array, elem)
{
	var i = 0;
	while (i < array.length) {
		if (array[i] == elem) {
			return;
		}
		i++;
	}
	
	array.push(elem);
}


/**
 *  removeElemIfExists
 * 
 *  Az eljárás töröl egy elemet a megadott tömbből, ha az benne van a
 *  tömbben(létezik).
 * 
 *  @param array - a tömb, amiből törölni szeretnénk
 *  @param elem  - az elem, amit törölni akarunk
**/
function removeElemIfExists(array, elem)
{
	var p = (-1);
		for (var i = 0; i < array.length && p == -1; i++) {
		if (array[i] == elem) {
			p = i;
		}
	}
	
	if (p != -1) {
		array.splice(p,1); 
	}
}

/**
 *  createListForRow
 * 
 *  A függvény megadja a csoportokat tartalmazó listát egy megadott
 *  sorhoz.
 * 
 *  @param row - a játékmező sorindexe
**/
function createListForRow(row)
{
	var start = (-1);
	var list = [];
	
	for (var i = 0; i < gSolution.length; i++) {
		if (gSolution[row][i] && start == (-1)) {
			start = i;
		}
		if (!gSolution[row][i] && start != (-1)) {
			list.push(i - start);
			start = (-1);
		}
	}
	
	if (start != (-1)) {
		list.push(gSolution.length - start);
	}
	
	return list;
}


/**
 *  createListForCol
 * 
 *  A függvény megadja a csoportokat tartalmazó listát egy megadott
 *  oszlophoz.
 * 
 *  @param col - a játékmező oszlopindexe
**/
function createListForCol(col)
{
	var start = (-1);
	var list = [];

	for (var i = 0; i < gSolution.length; i++) {
		if (gSolution[i][col] && start == (-1)) {
			start = i;
		}
		if (!gSolution[i][col] && start != (-1)) {
			list.push(i - start);
			start = (-1);
		}
	}

	if (start != (-1)) {
		list.push(gSolution.length - start);
	}
	
	return list;
}


/**
 *  getFieldByXY
 * 
 *  A függvény megadja a megadott mező objektumát.
 * 
 *  @param row - a keresett mező sorindexe
 *  @param col - a keresett mező oszlopindexe
**/
function getFieldByXY(row, col)
{
	var size = gGameField.length;
	var field_idx = Math.floor(row / 5) * (size / 5) + Math.floor(col / 5);
	var tbody = $('nagymezo' + field_idx).firstChild.firstChild;

	return tbody.children[row % 5].children[col % 5];
}


/**
 *  checkForRowCompletion
 * 
 *  A függvény megvizsgálja a megadott sort és eldönti, hogy minden
 *  fekete mezőcsoport el lett-e helyezve a játékmezőn az adott sor
 *  mellett.
 * 
 *  @param row - az ellenőrizendő sorindex
**/
function checkForRowCompletion(row)
{
	var start = (-1);
	var list = [];

	for (var i = 0; i < gGameField.length; i++) {
		if (gGameField[row][i] == 1 && start == (-1)) {
			start = i;
		}
		if (gGameField[row][i] != 1 && start != (-1)) {
			list.push(i - start);
			start = (-1);
		}
	}

	if (start != (-1)) {
		list.push(gGameField.length - start);
	}

	var status = areListsEqual(list, gRowLists[row])
	paintListOfRow(row, status);

	if (status) {
		addElemIfNotExists(gComplete, 'R' + row);
	}
	else {
		removeElemIfExists(gComplete, 'R' + row);
	}

	return status;
}


/**
 *  checkForColCompletion
 * 
 *  A függvény megvizsgálja a megadott oszlopot és eldönti, hogy minden
 *  fekete mezőcsoport el lett-e helyezve a játékmezőn az adott
 *  oszlop alatt.
 * 
 *  @param col - az ellenőrizendő oszlopindex
**/
function checkForColCompletion(col)
{
	var start = (-1);
	var list = [];

	for (var i = 0; i < gGameField.length; i++) {
		if (gGameField[i][col] == 1 && start == (-1)) {
			start = i;
		}
		if (gGameField[i][col] != 1 && start != (-1)) {
			list.push(i - start);
			start = (-1);
		}
	}

	if (start != (-1)) {
		list.push(gGameField.length - start);
	}

	var status = areListsEqual(list.reverse(), gColLists[col]);
	paintListOfCol(col, status);

	if (status) {
		addElemIfNotExists(gComplete, 'C' + col);
	}
	else {
		removeElemIfExists(gComplete, 'C' + col);
	}

	return status;
}


/**
 *  checkForCompletion
 * 
 * Ellenőrzi, hogy a játék meg lett-e oldva.
**/
function checkForCompletion()
{
	var len = gGameField.length;
	
	if (gComplete.length == 2 * len) {
		if (!gQuickMode) {
			for (var i = 0; i < len; i++) {
				for (var j = 0; j < len; j++) {
					if (gGameField[i][j] == 0) {
						return;
					}
				}
			}
		}

		onPlayerReady(true);
	}
}


//----------------------------------------------------------------------
//  DOM:
// Kezelőfelület, játétér kialakítása:
//----------------------------------------------------------------------


/**
 *  paintListOfRow
 * 
 *  Az eljárás a megadott sorhoz tartozó számlistát kiszínezi feketére
 *  vagy szürkére.
 * 
 *  @param row - a színezendő sorindex
 *  @param done - a sor állapota (igaz, ha minden csoport kész)
**/
function paintListOfRow(row, done)
{
	var field_idx = Math.floor(row / 5);
	var tds = $('balmezo' + field_idx).firstChild.firstChild.children[row % 5].children;
	
	for (var i = 0; i < tds.length; i++) {
		if (tds[i].innerHTML) {
			tds[i].className = done ? 'field-grey' : 'field-black';
		}
	}
}


/**
 *  paintListOfCol
 * 
 *  Az eljárás a megadott oszlophoz tartozó számlistát színezi
 *  feketére vagy szürkére.
 * 
 *  @param col - a színezendő oszlopindex
 *  @param done - az oszlop állapota (igaz, ha minden csoport kész)
**/
function paintListOfCol(col, done)
{
	var field_idx = Math.floor(col / 5);
	var rows = $('topmezo' + field_idx).firstChild.firstChild.children;
	
	for (var i = 0; i < rows.length; i++) {
		if (rows[i].children[col % 5].innerHTML) {
			rows[i].children[col % 5].className = done ? 'field-grey' : 'field-black';
		}
	}
}


/**
 *  increaseClock
 * 
 *  Óra megjelenítése.(játékidő)
**/
function increaseClock()
{
	if (gPaused) return;
	
	gPlayTime += 1;
	
	var sec  = gPlayTime % 60;
	var min  = ((gPlayTime - sec) / 60) % 60;
	var hour = (((gPlayTime - sec) / 60) - min) / 60;
	
	$('clock').innerHTML = pad00(hour) + ':' + pad00(min) + ":" + pad00(sec);
}


/**
 *  generateGameField
 * 
 *  Létrehozza a játékfelületet, a hozzá szükséges HTML elemeket és
 *  beállítja a megfelelő eseményekzelőket.
 * 
 *  @param container - a játékmezőt ide töltjük be
 *  @param size      - a játékmező mérete
**/
function generateGameField(container, size)
{
	var maxRowListLength = getLongestListLength(gRowLists);
	var maxColListLength = getLongestListLength(gColLists);

	for (var i = 0; i < size; i++) {
		var nagymezo = document.createElement("div");
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");

		for (var j = 0; j < maxColListLength; j++) {
			var tr = document.createElement("tr");
			for (var k = 0; k < 5; k++) {
				var td = document.createElement("td");
				td.innerHTML = '';
				tr.appendChild(td);
			}
			tbody.appendChild(tr);
		}
		
		for (var j = 0; j < 5; j++) {
			var list = gColLists[i * 5 + j].reverse();
			for (var k = 0; k < list.length; k++) {
				var td = tbody.children[maxColListLength - 1 - k].children[j];
				td.innerHTML = list[k];
				td.className = 'field-black';
			}
			if (list.length == 0) {
				gComplete.push('C'+(i * 5 + j));
			}
		}

		nagymezo.id = 'topmezo'+ i;

		table.appendChild(tbody);
		nagymezo.appendChild(table);
		$('gameTop').appendChild(nagymezo);
	}

	for (var i = 0; i < size; i++) {
		var nagymezo = document.createElement("div");
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");

		for (var j = 0; j < 5; j++) {
			var tr = document.createElement("tr");
			var list = gRowLists[i*5+j];
			if (list.length == 0) {
				gComplete.push('R'+(i * 5 + j));
			}
			var off = maxRowListLength - list.length;
			for (var k = 0; k < maxRowListLength; k++) {
				var td = document.createElement("td");
				td.innerHTML = '';
				if (k >= off) {
					td.innerHTML = list[k - off];
					td.className = 'field-black';
				}
				tr.appendChild(td);
			}
			tbody.appendChild(tr);
		}
		
		nagymezo.id = "balmezo"+i;

		table.appendChild(tbody);
		nagymezo.appendChild(table);
		$('gameLeft').appendChild(nagymezo);

		var fix = document.createElement("div");
		fix.className = 'cl';
		$('gameLeft').appendChild(fix);
	}
	
	for (var i = 0; i < size * size; i++) {
		var nagymezo = document.createElement("div");
		var table = document.createElement("table");
		var tbody = document.createElement("tbody");
		
		for (var j = 0; j < 5; j++) {
			var tr = document.createElement("tr");
			for (var k = 0; k < 5; k++) {
				var td = document.createElement("td");
				tr.appendChild(td);
				td.onclick = onPlayerClickedOnField;
				
			}
			tbody.appendChild(tr);
		}
		
		nagymezo.id = 'nagymezo' + i;

		table.appendChild(tbody);
		nagymezo.appendChild(table);
		container.appendChild(nagymezo);
		
		if ((i + 1) % size == 0) {
			var fix = document.createElement("div");
			fix.className = 'cl';
			container.appendChild(fix);
		}
	}
}


//----------------------------------------------------------------------
//  Eseménykezelés
//----------------------------------------------------------------------



/**
 *  onPlayerReady
 * 
 *  Akkor hívódik meg, amikor a játékos minden mezőt feketére vagy
 *  szürkére szinez. Helyes megoldás esetén megjelenik egy
 *  sikert jelző ablak, helytelen megoldás esetén figyelmeztet.
 * 
 *  @param manual - ha igaz, akkor a játékos saját maga oldotta meg a
 *                  feladatot, egyébként a Megoldás gombra kattintott
**/
function onPlayerReady(manual)
{
	if (manual) {
		window.scrollTo(0, 0);
		$('congrat-layer').style.display = 'block';
	}
	
	gSolved = true;
	window.clearInterval(gGameTimer);

	$('button-play-pause').innerHTML = 'Újrakezdés';
}


/**
 *  onPlayerClickedOnField
 * 
 *  Akkor hívódik meg, amikor a játékos rákattint egy rublikára a
 *  játékmezőn. A függvény a rublika állapotát megváltoztatja, ha
 *  a játék nem szünetel vagy nem ért véget.
**/
function onPlayerClickedOnField()
{
	if (gSolved || gPaused) return true;

	var size = gGameField.length / 5;
	var mezo = this.parentNode.parentNode.parentNode.parentNode.id.substr(8);
	var oszlop = this.cellIndex + (mezo % size) * 5;
	var sor = this.parentNode.rowIndex + Math.floor(mezo / size) * 5;

	gGameField[sor][oszlop]++;
	gGameField[sor][oszlop] %= 3;

	switch (gGameField[sor][oszlop]) {
		case 0: {
			this.className = 'field-normal';
			break;
		}
		case 1: {
			this.className = 'field-black';
			break;
		}
		case 2: {
			this.className = 'field-grey';
			break;
		}
	}

	checkForRowCompletion(sor);
	checkForColCompletion(oszlop);

	checkForCompletion();
}


/**
 * 
 * 
 * 
**/
function onPlayerClickedCongratLayer()
{
	$('congrat-layer').style.display = 'none';
}


/**
 *  onCheckFields
 * 
 *  Akkor hívódik meg, amikor a játékos ellenőrzést kér a mezőkön
 *  elhelyezett állapotokra. A helytelen állapottal megjelölt mező
 *  színét pirosra változtatja, a fehér mezĹket figyelmen kívül hagyja.
**/
function onCheckFields()
{
	var size = gGameField.length;
	
	for (var i = 0; i < size; i++) {
		for (var j = 0; j < size; j++) {
			if (gGameField[i][j] != 0) {
				if (gGameField[i][j] == 1 && gSolution[i][j] == false ||
					gGameField[i][j] == 2 && gSolution[i][j] == true) {
					getFieldByXY(i, j).className = 'field-wrong';
				}
			}
			else {
				getFieldByXY(i, j).className = 'field-normal';
			}
		}
	}
}


/**
 *  onSmallGameSelected
 * 
 *  Akkor hívódik meg, amikor a játékos a kis méretű feladványt
 *  választja (gombbal vagy a nyitó-oldalon).
**/
function onSmallGameSelected()
{
	onInitGame(2);
	generateGameField($('gameplay'), 2);
	
	$('sign').innerHTML = 'Kis méretű feladvány';
}


/**
 *  onLargeGameSelected
 * 
 *  Akkor hívódik meg, amikor a játékos a közepes méretű feladványt
 *  választja (gombbal vagy a nyitó-oldalon).
 *  
**/
function onLargeGameSelected()
{
	onInitGame(3);
	generateGameField($('gameplay'), 3);

	$('sign').innerHTML = 'Közepes méretű feladvány';
}


/**
 *  onResetGame
 * 
 *  Akkor hívódik meg, amikor a játékos új játékot akar kezdeni. A
 *  függvény eltakarít minden szükségtelen információt, ábrát és
 *  alapértelmezettre állítja a tömböket és a változókat.
**/
function onResetGame()
{	
	$('gameplay').innerHTML = '';
	$('gameTop').innerHTML = '';
	$('gameLeft').innerHTML = '';
	
	window.clearInterval(gGameTimer);
	
	$('clock').innerHTML = '00:00:00';

	gGameField = [];
	gSolution = [];
	gRowLists = [];
	gColLists = [];
	gGameTimer = null;
	gPlayTime = 0;
	gSolved = false;
	gPaused = false;
	gComplete = [];

	$('button-play-pause').innerHTML = 'Szünet';
}


/**
 *  onRestartGame
 *  
 *  Akkor hívódik meg, amikor a játékos újra akarja ugyanazt a
 *  feladványt játszani.
**/
function onRestartGame()
{
	if (gGameField.length == 15) onLargeGameSelected();
	else onSmallGameSelected();
}


/**
 *  onPlayPauseGame
 * 
 *  Akkor hívódik meg, amikor a játékos megállítja vagy szünetelteti
 *  a játékot. Ha a játék már véget ért, akkor a játékos új játékot
 *  kér. Az eljárás végrehajtja a kérést, és a gomb szövegét lecseréli.
**/
function onPlayPauseGame()
{
	if (gSolved) {
		onRestartGame();
		return;
	}
	
	if (gPaused) {
		this.innerHTML = 'Szünet';
	}
	else {
		this.innerHTML = 'Folytatás';
	}

	gPaused = !gPaused;
}


/**
 *  onShowSolution
 * 
 *  Akkor hívódik meg, amikor a játékos a megoldás megmutatását kéri.
**/
function onShowSolution()
{
	var size = gGameField.length;
	
	for (var i = 0; i < size; i++) {
		for (var j = 0; j < size; j++) {
			if (gSolution[i][j]) {
				getFieldByXY(i, j).className = 'field-black';
				gGameField[i][j] = 1;
			}
			else {
				getFieldByXY(i, j).className = 'field-grey';
				gGameField[i][j] = 2;
			}
		}
	}

	var toplists = $('gameTop').children;
	size = toplists.length;

	for (var i = 0; i < size; i++) {
		var trs = toplists[i].firstChild.firstChild.children;
		for (var j = 0; j < trs.length; j++) {
			var tds = trs[j].children;
			for (var k = 0; k < tds.length; k++) {
				if (tds[k].innerHTML) {
					tds[k].className = 'field-grey';
				}
			}
		}
	}

	var leftlists = $('gameLeft').children;
	size = leftlists.length;

	for (var i = 0; i < size; i+=2) {
		var trs = leftlists[i].firstChild.firstChild.children;
		for (var j = 0; j < trs.length; j++) {
			var tds = trs[j].children;
			for (var k = 0; k < tds.length; k++) {
				if (tds[k].innerHTML) {
					tds[k].className = 'field-grey';
				}
			}
		}
	}
	
	onPlayerReady(false);
}


/**
 *  onInitGame
 * 
 *  Akkor hívódik meg, amikor a játékos játékot kezd (választ
 *  egy feladványt). Az eljárás előkészíti a játékhoz szükséges
 *  tömböket és változókat.
**/
function onInitGame(size)
{
	var n = size;
	
	if (gGameField != []) {
		onResetGame();
	}	
	
	$('welcome-page').style.display = 'none';
	$('game-page').style.display    = 'inherit';
	//jQuery('#game-page').slideDown();

	if (size == 2) {
		solution = gSmallGame;
	}
	else if (size == 3) {
		solution = gLargeGame;
	}
	
	gGameTimer = window.setInterval(increaseClock, 1000);

	for (var i = 0; i < size * 5; i++) {
		gGameField[i] = [];
		gSolution[i] = [];
		for (var j = 0; j < size * 5; j++) {
			gGameField[i][j] = 0;
			gSolution[i][j] = solution[i][j] == '1';
		}
	}
	
	for (var i = 0; i < size * 5; i++) {
		gRowLists[i] = createListForRow(i);
		gColLists[i] = createListForCol(i);
	}
}


/**
 *  onLoadGame
 * 
 *  Akkor hívódik meg, amikor az oldal betöltődött. Felhasználói
 *  felületet (UI) alkot majd a gombokhoz rendel esemenykezelőket.
**/
function onLoadGame()
{
	$('button-check-fields').onclick = onCheckFields;
	$('button-show-solution').onclick = onShowSolution;
	$('button-small-game').onclick = onSmallGameSelected;
	$('button-large-game').onclick = onLargeGameSelected;
	$('button-play-pause').onclick = onPlayPauseGame;

	$('select-small-game').onclick = onSmallGameSelected;
	$('select-large-game').onclick = onLargeGameSelected;
	
	$('congrat-layer').onclick = onPlayerClickedCongratLayer;
	
	gQuickMode = window.location.search.substr(1) == 'quick';
};


window.onload = onLoadGame;