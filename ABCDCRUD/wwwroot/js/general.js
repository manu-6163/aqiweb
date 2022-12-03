function CheckNumeric(e) {
	if (window.event) // IE
	{
		if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 && e.keyCode != 44) {
			event.returnValue = false;
			return false;
		}
	}
	else { // Fire Fox
		if ((e.which < 48 || e.which > 57) & e.which != 8 && e.which != 44) {
			e.preventDefault();
			return false;
		}
	}
}