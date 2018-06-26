<?php
include ('Functions.php');
 ?>
<!DOCTYPE html>
<html class="html">
	<?php 
	ini_set("default_charset","utf-8");
	Functions::createHeader(); ?>
	<body>
		<?php Functions::generateCongratPopUp(); ?>
		<div class="head">
			<div id = "menu">
				<?php Functions::generateMenu(FALSE,""); ?>
			</div>
			<div class="wrapper">
				<?php
				Functions::generateRules(); ?>	
			</div>
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
