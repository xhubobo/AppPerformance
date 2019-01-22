echo begin to delete bin and obj folders
for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s /q "%%d"
echo begin to delete .pdb files
del .\Output\Debug\*.pdb
pause