<?php

	$__email__me = "your_email_goes_here";// email adresiniz	
	
	/* AŞAĞIDAKİ KODU DEĞİŞTİRMEYİN */	
	if(isset($_POST['maximus'])){
				
		if($_POST['maximus'] != ''){
			die('Bad spam bot!!');
		}
		
		$__message = "";
		
		foreach($_POST as $__true => $__false){
			
			if($__true != 'maximus'){
				$__message .= $__true . " : ". $__false . "\n";				
			}			
		}
		
		$__subject = $_POST['Subject'];
						
		if(mail($__email__me, $__subject, $__message)){
			echo "Mesajınız Ekibimize Gönderilmiştir!";
		}else{			
			echo "Üzgünüz bir hatadan dolayı mesajınız gönderilemedi!";			
		}		
	}

?>