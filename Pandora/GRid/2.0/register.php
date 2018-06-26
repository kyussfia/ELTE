<?php
include ('Functions.php');
/*
A játék csak regisztrált és bejelentkezett felhasználóknak legyen elérhető. Regisztráció során a felhasználónév és jelszó mellett kérjük be a teljes nevét és e-mail címét is az illetőnek. A beviteli mezők helyességét ellenőrizni is kell: felhasználónév minimum 4 karakter hosszú, e-mail cím helyessége, valamint a teljes név csak betűkből állhat.

Bejelentkezés után listázzuk ki a rendszerben tárolt feladványokat, feltüntetve a méreteit, a leggyorsabb megoldás idejét, a leggyorsabb megoldó nevét, valamint azt, hogy megoldottuk-e már ezt a rejtvényt.

Egy rejtvényre kattintva legyen lehetőség annak megoldására (ld. JavaScript beadandó). Ha megoldás közben semmilyen segítséget nem kért a felhasználó, akkor AJAX technológiával küldjük el a szerverre a megoldás idejét, válaszként pedig az adott rejtvény megoldók közül a 10 legjobbat adjuk vissza és jelenítsük meg (top 10).

Admin felhasználónak (speciális felhasználónév jelöli ki) legyen lehetősége új grafilogikai feladványt szerkesztenie. Ehhez a méretek megadása után a megoldó felülethez teljesen hasonló táblázatban kell fekete-fehér négyzetekből képet rajzolnia. Itt csak két állapota lehet egy négyzetnek: vagy van ott pont, vagy nincs (fekete vagy fehér). Mentés gombra kattintva a képet elmentjük szerveroldalra.

Az adatok tárolása fájlban történik.
*/


?>
<!DOCTYPE html>
<html class="html">
	<?php 
	ini_set("default_charset","utf-8");
	Functions::createHeader(); ?>
	<body>
		<?php Functions::generateCongratPopUp(); ?>
		<div class="head">
			<?php Functions::generateRegisterField(); ?>
		</div>
		
		<div class="gamefield">
			<?php Functions::generateGamePage(); ?>
		</div>
		
		<div class="foot">
			<?php Functions::generateFooter(); ?>
		</div>
	</body>
	<?php Functions::attachJsFile(); ?>
</html>

