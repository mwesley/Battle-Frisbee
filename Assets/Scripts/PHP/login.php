<?php
	$user = $_GET['username'];
	$pass = $_GET['password'];

	$sqlconnection = mysqli_connect("mysql.hazelhosting.com", "u884179854_wess", "password", "u884179854_fris");
	if(mysqli_connect_errno()) {
		echo "failed to connect".mysqli_connect_error();
	}

	if(isset($user) && isset($pass)){
		$query = "SELECT username,wins,losses FROM users WHERE Username = '".$user."' and Password = '".$pass."'";
		
		$result = mysqli_query($sqlconnection, $query);


		if($result->num_rows == 0) {
			echo "Nope";
		} else {
			echo "success.";
			while($row = $result->fetch_assoc()) {
				echo $row["wins"]. ".";
				echo $row["losses"];
				}
		}
	}
?>