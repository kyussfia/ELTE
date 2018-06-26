<?php
Class Functions {
	public function createHeader() {
		echo '<head><title>GRIDDLER - Grafilogika - Webfejl. 2 bead. (M.M.) </title><meta name="keywords" content="web-fejlesztés,grafilogika,griddler,mikus márk,javascript beadandó"><meta name="author" content="Mikus Márk (CM6TSV)"><link rel="stylesheet" type="text/css" href="bead.css"></head>';
	}
	
	public function generateCongratPopUp() { 
		echo '<div class="popup-layer" id="congrat-layer"><div class="popup-window"><img src="iconmonstr-check-mark-5-icon.png" alt="Gratulálok!"><strong>Gratulálok!</strong><br>Sikeresen megfejtetted a feladványt!</div></div>';
	}
	
	public function attachJsFile() {
		echo '<script type="text/javascript" src="jquery-1.11.0.js"></script><script type="text/javascript" src="bead.js"></script><link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css">';
	}
	
	public function generateMenu($loggedIn,$username) {
		if($loggedIn) {
			echo '<form acion="logOff.php" method="post" class="form-horizontal">
					Üdvözöllek,:&nbsp;' . $username . '
				  <br>
				  <br>
				  <br>
					  <button type="submit" class="btn">Log Off</button>
					</div>
				  </div>
				</form>';
		} else {
		echo '<form action="checkUser.php" method="post" class="form-horizontal"><div class="control-group"><label class="control-label" for="inputEmail">Email</label><div class="controls"><input type="text" name="user" id="inputEmail" placeholder="Email"></div></div><div class="control-group"><label class="control-label" for="inputPassword">Password</label><div class="controls"><input type="password" name="pwd" id="inputPassword" placeholder="Password"></div></div><div class="control-group"><div class="controls"><label class="checkbox">Remember me <input name="rememberMe" id="rememberMe" type="checkbox"></label><button type="submit" class="btn">Sign in</button></div></div></form><br><a href="register.php">Or Register</a>';
		}
	}
	
	public function generateRules() {
		echo '<div id="leftmrg"><h1>Griddler / Grafilogika / JavaScript beadandó</h1><p	><strong>A szabályok</strong> egyszerűek:<br><br><br>	
					Kapsz egy négyzethálót, melyben a mezőket be kell satíroznod feketére (1 klikk). Minden sor mellett megadom az abban a sorban található, egybefüggő fekete blokkok hosszát. Ugyanígy az oszlopok tetején található az abban az oszlopban levő, egybefüggő fekete blokkok hossza. A feladatod minden fekete mező besatírozása.Bal klikkre besatírozod a mezőt feketére. 2-szer bal-klikkel tudod szürkére megjelölni. A fekete jelenti, hogy arra a mezőre biztosan kerül jelölés, a szüke azt jelenti hogy oda biztosan nem kerül semmi. Alapállapotban minden mező fehér,ez azt jelenti, hogy ott még nincs eldötve semmi. Ha jól dolgzotál a megoldás egy kép lesz.<br><br> (1klikk=fekete, 2klikk=szürke, 3klikk=fehér)</p>
					</div>
				<div class="sign" id="sign">Válassz feladványt!</div>';
	}
	
	public function generateGamePage() {
		echo '<div id="game-page" class="game-page">
				<div class="game-main">
					<div id="gamefield">
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
					<a id="button-check-fields" href="javascript:void(0)" class="button-blue">Ellenőrzés</a>
					<a id="button-show-solution" onclick="javascript:void(0)" class="button-green">Megoldás</a>
					<a id="button-play-pause" href="javascript:void(0);" class="button-green">Szünet</a>
					<div class="button-title">Feladványok</div>
					<div id="button-small-game" class="button-green">Kis méretű feladvány</div>
					<div id="button-large-game" class="button-green">Közepes méretű feladvány</div>
				</div>
				<div class="cb">&nbsp;</div>
			</div>';
	}
	
	public function generateFooter() {
		echo '<div id="welcome-page" class="home-page">
				<div class="size1of2 unit">
					<div id="select-small-game">Kis méretű feladvány</div>
				</div>
				<div class="size2of2 unit">
					<div id="select-large-game">Közepes méretű feladvány</div>
				</div>
				<div class="cb">&nbsp;</div>
			</div>
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
				<div class="size2of2 unit">
					<h3>Készítette</h3>
					<p>Név: <strong>Mikus Márk</strong><br>Neptun-kód: <strong>CM6TSV</strong></p>
					<h3>Megjegyések:</h3>
					<p>Készült az <a href="http://webprogramozas.inf.elte.hu/gyak/js_beadando.html" target="_blank">ELTE-IK Web-fejlesztés 2. JavaScript beadandó</a> feladatleírása alapján.</p>
					<p>Az oldal szabványos <strong>HTML5</strong> illetve <strong>CSS3</strong> fájlokból áll.</p>
				</div>
				<div class="cb">&nbsp;</div>
			</div>';
	}
	public function generateRegisterField() {
		echo '<form id="registerForm" action="checkUser.php?register=true" method="post" class="form-horizontal">
			  <div class="control-group">
				<label class="control-label" for="inputUsername">Felhasználónév:</label>
				<div class="controls">
				  <input type="text" name="username" id="inputUsername" placeholder="Username">
				</div>
			  </div>
			  <div class="control-group">
				<label class="control-label" for="inputFullname">Teljes Név:</label>
				<div class="controls">
				  <input type="text" name="fullname" id="inputFullname" placeholder="Full Name">
				</div>
			  </div>
			  <div class="control-group">
				<label class="control-label" for="inputEmail">Email cím:</label>
				<div class="controls">
				  <input type="text" name="email" id="inputEmail" placeholder="Email">
				</div>
			  </div>
			  <div class="control-group">
				<label class="control-label" for="inputPassword">Jelszó:</label>
				<div class="controls">
				  <input type="password" name="pwd" id="inputPassword" placeholder="Password">
				</div>
			  </div>
			  <div class="control-group">
				<div class="controls">
				  <button type="submit" class="btn">Register</button>
				</div>
			  </div>
		</form>';
	
	}
	
	
}

?>