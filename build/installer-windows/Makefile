all:
	makensis -INPUTCHARSET UTF8 install-script.nsi

zip:
	rm -f suika2.zip
	zip -r suika2.zip suika-pro.exe games tools plaintext.code-snippets.en plaintext.code-snippets.jp

clean:
	rm -rf suika-pro.exe games tools suika2-installer.exe
