$szoveg=read-host "Add meg a szöveget:"
$i=0
while ($i -lt $szoveg.length)
{
write-host $szoveg[$i]
$i++
}