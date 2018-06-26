$f = Get-Content "input.txt";
$new = "";
foreach ($line in $f) {
    $new += $line;
}
for ($i = 1; $i -le 5; $i++) {
    $l = [System.Text.RegularExpressions.Regex]::Replace($new, '[^' + $i + ']', '').Length;
    Write-Host $i": "$l;
}