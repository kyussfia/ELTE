<?php
	if($_POST){
			$logout = ($_POST['logout']);
			if ($logout) {
				header('Location: login.html');
				exit();
			}
			
		}
?>
<!DOCTYPE html>
<html lang="hu">
	<head>
		<title>GRIDDLER - Grafilogika - Webfejl. 2 bead. (M.M.) </title>
		<meta charset="utf-8">
		<meta name="keywords" content="web-fejlesztés,grafilogika,griddler,mikus márk,javascript beadandó">
		<meta name="author" content="Mikus Márk (CM6TSV)">
		<link rel="stylesheet" type="text/css" href="bead.css">
		<script type="text/javascript" src="bead.js"></script>
	</head>
	<body>
		<div class="popup-layer" id="congrat-layer">
			<div class="popup-window">
				<img src="iconmonstr-check-mark-5-icon.png" alt="Gratulálok!">
				<strong>Gratulálok!</strong>
				<br>Sikeresen megfejtetted a feladványt!
			</div>
		</div>
		<div class="head">
			<div class = "left-menu" id= "menu">
				<form action="" method ="post">
				
					<? session_start(); echo "Szia, {$_SESSION['u_name']}<br>";	
					echo "felhasználo neved: {$_SESSION['user_name']}"; ?>
						
						<br><input type="submit" name="logout" title="Logout" value="Kijelentkezes">
						
						d
				</form>	
			
			</div>
			<div class="wrapper">
				<div id="leftmrg">
				<h1>Griddler / Grafilogika / JavaScript beadandó</h1>
				<p><strong>A szabályok</strong> egyszerűek:<br>
					Kapsz egy négyzethálót, melyben a mezőket be kell satíroznod feketére (1 klikk). Minden sor mellett megadom az abban a sorban található, egybefüggő fekete blokkok hosszát. Ugyanígy az oszlopok tetején található az abban az oszlopban levő, egybefüggő fekete blokkok hossza. A feladatod minden fekete mező besatírozása.Bal klikkre besatírozod a mezőt feketére. 2-szer bal-klikkel tudod szürkére megjelölni. A fekete jelenti, hogy arra a mezőre biztosan kerül jelölés, a szüke azt jelenti hogy oda biztosan nem kerül semmi. Alapállapotban minden mező fehér,ez azt jelenti, hogy ott még nincs eldötve semmi. Ha jól dolgzotál a megoldás egy kép lesz. (1klikk=fekete, 2klikk=szürke, 3klikk=fehér(újra))</p>
					</div>
				<div class="sign" id="sign">Válassz feladványt!</div>	
			</div>
		</div>
		
		<div class="wrapper">
			<div id="welcome-page" class="home-page">
				<div class="size1of2 unit">
					<a href="javascript:void(0);" id="select-small-game">Kis méretű feladvány</a>
				</div>
				<div class="size1of2 unit">
					<a href="javascript:void(0);" id="select-large-game">Közepes méretű feladvány</a>
				</div>
				<div class="cb">&nbsp;</div>
			</div>
			<div id="game-page" class="game-page">
				<div id="gamefield" class="game-main">
					<div>
						<div id="gameTop" class="game-top"></div>
						<div class="cb">&nbsp;</div>
					</div>
					<div id="gameLeft" class="game-left"></div>
					<div id="gameplay" class="game-play"></div>
				</div>
				<div class="button-area">
					<div class="clock-box">
						<div class="clock-title">Játékidő</div>
						<div class="clock" id="clock">00:00:00</div>
					</div>
					<a id="button-check-fields" href="javascript:void(0);" class="button-blue">Ellenőrzés</a>
					<a id="button-show-solution" href="javascript:void(0);" class="button-green">Megoldás</a>
					<a id="button-play-pause" href="javascript:void(0);" class="button-green">Szünet</a>
					<div class="button-title">Feladványok</div>
					<a id="button-small-game" href="javascript:void(0);" class="button-green">Kis méretű feladvány</a>
					<a id="button-large-game" href="javascript:void(0);" class="button-green">Közepes méretű feladvány</a>
				</div>
				<div class="cb">&nbsp;</div>
			</div>
		</div>
		
		<div class="foot">
			<div class="wrapper">
				<div class="size1of2 unit">
					<h3>Technikai információk</h3>
					<p>Források:</p>
					<ul>
						<li><a href="bead.html"><strong>HTML-dokumentum</strong>(92 sor)</a></li>
						<li><a href="bead.js"><strong>JavaScript fájl</strong>(886 sor)</li></a>
						<li><a href="bead.css"><strong>CSS stíluslap</strong>(350 sor)</li></a>
					</ul>
					<p>Tesztelve:</p>
					<ul>
						<li><strong>Google Chrome</strong></li>
						<li><strong>Mozilla Firefox</strong></li>
						<li><strong>Opera</strong></li>
						<li><strong>Internet Explorer</strong></li>
					</ul>
				</div>
				<div class="size1of2 unit">
					<h3>Készítette</h3>
					<p>Név: <strong>Mikus Márk</strong><br>Neptun-kód: <strong>CM6TSV</strong></p>
					<h3>Megjegyések:</h3>
					<p>Készült az <a href="http://webprogramozas.inf.elte.hu/gyak/js_beadando.html" target="_blank">ELTE-IK Web-fejlesztés 2. JavaScript beadandó</a> feladatleírása alapján.</p>
					<p>Az oldal szabványos <strong>HTML5</strong> illetve <strong>CSS3</strong> fájlokból áll.</p>
				</div>
				<div class="cb">&nbsp;</div>
			</div>
		</div>
	</body>
</html>
