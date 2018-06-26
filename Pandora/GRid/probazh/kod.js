var dbs=document.getElementsByName('db'); //az összes "db" input lekérdezése
//megj.: lehetne úgy is, hogy egy eseménykezelőt viszünk fel egy szülő elemhez (pl. magához a táblázathoz) és ott ellenőrizzük, hogy az esemény forrása (event.target) egy "db" nevű inputtól származik-e.
for(var i=0;i<dbs.length;++i){
	dbs[i].onchange=function(e){
		var val=parseInt(e.target.value); //számmá alakítjuk a mező tartalmát
		e.target.value=val; //visszaírjuk a számot a mezőbe, mert ha pl. "12asd" szöveget viszünk be, azt a parseInt 12-vé alakítja, és nem vesszük észre, hogy nem egészen szám
		if(isNaN(val)){ //ha nem számot írtunk be
			e.target.value=1;
		}//ha 1-nél kisebb számot vittünk be
		if(val<=0){
			e.target.value=1;
		}
	}
} 

document.getElementById('megegyezik').onclick=function(e){
	var d=document.getElementById('sz').style; //a számlázási címet tartalmazó div stílusa
	if(e.target.checked){ //ha a megegyezik checkbox be van pipálva
		d.display='none';
	}else{
		d.display='inherit';
	}
}

document.getElementById('f').onsubmit=function(){ //form elküldésére eseménykezelőt veszünk fel
	if(document.getElementById('v').value==''){
		alert('Vezetéknév kötelező'); //A hiba kezelésére más megoldás is létezhet, pl. egy div innerHTML-jének beállítása, vagy az input mező className paraméterének átállítása
		return false;	//ha valami hibát tapasztalunk, a "return false;" segítségével megakadályozzuk a form tényleges elküldését
	}
	if(document.getElementById('k').value==''){
		alert('Keresztnév kötelező');
		return false;
	}
	if(document.getElementById('s').value==''){
		alert('Szállítási cím kötelező');
		return false;
	}

}