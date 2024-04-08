DESTDIR=/usr/local

build: suika-linux suika-pro

suika-linux:
	@# Guard if macOS.
	@if [ ! -z "`uname | grep Darwin`" ]; then \
		echo 'You cannot run Makefile on macOS.'; \
		exit 1; \
	fi;
	@# For Linux:
	@if [ ! -z "`uname | grep Linux`" ]; then \
		cd build/engine-linux && \
		make -f Makefile.linux -j8 && \
		make -f Makefile.linux install && \
		cd ../..; \
	fi
	@# For FreeBSD:
	@if [ ! -z "`uname | grep FreeBSD`" ]; then \
		cd build/engine-linux && \
		gmake -f Makefile.freebsd -j8 && \
		gmake -f Makefile.freebsd install && \
		cd ../..; \
	fi
	@# For NetBSD:
	@if [ ! -z "`uname | grep NetBSD`" ]; then \
		cd build/engine-linux && \
		gmake -f Makefile.netbsd -j8 && \
		gmake -f Makefile.netbsd install && \
		cd ../..; \
	fi
	@# For OpenBSD:
	@if [ ! -z "`uname | grep OpenBSD`" ]; then \
		cd build/engine-linux && \
		gmake -f Makefile.openbsd -j8 && \
		gmake -f Makefile.openbsd install && \
		cd ../..; \
	fi

suika-pro:
	@if [ ! -z "`uname | grep Darwin`" ]; then \
		echo 'You cannot run Makefile on macOS.'; \
		exit 1; \
	fi;
	@cd build/pro-linux && \
		./make-deps.sh && \
		rm -rf build && \
		mkdir build && \
		cd build && \
		cmake .. && \
		make && \
		cp suika-pro ../../../ && \
		cd ../../..

install: build
	@install -v -d $(DESTDIR)/bin
	@install -v suika-linux $(DESTDIR)/bin/suika-runtime
	@install -v suika-pro $(DESTDIR)/bin/suika2

	@install -v -d $(DESTDIR)/share
	@install -v -d $(DESTDIR)/share/suika2

	@install -v -d $(DESTDIR)/share/suika2/export-linux
	@install -v suika-linux $(DESTDIR)/share/suika2/export-linux/suika-runtime

	@if [ ! -z "`uname | grep BSD`" ]; then install -v -d $(DESTDIR)/share/suika2/export-web; fi
	@if [ ! -z "`uname | grep BSD`" ]; then install -v build/engine-wasm/html/index.html $(DESTDIR)/share/suika2/export-web; fi
	@if [ ! -z "`uname | grep BSD`" ]; then install -v build/engine-wasm/html/index.js $(DESTDIR)/share/suika2/export-web; fi
	@if [ ! -z "`uname | grep BSD`" ]; then install -v build/engine-wasm/html/index.wasm $(DESTDIR)/share/suika2/export-web; fi

	@install -v -d $(DESTDIR)/share/suika2/japanese-light
	@cd games/japanese-light && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/japanese-light/{}" ';' && cd ../..
	@cd games/japanese-light && find . -type f -exec install -v "{}" "$(DESTDIR)/share/suika2/japanese-light/{}" ';' && cd ../..

	@install -v -d $(DESTDIR)/share/suika2/japanese-dark
	@cd games/japanese-dark && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/japanese-dark/{}" ';' && cd ../..
	@cd games/japanese-dark && find . -type f -exec install -v "{}" "$(DESTDIR)/share/suika2/japanese-dark/{}" ';' && cd ../..

	@install -v -d $(DESTDIR)/share/suika2/japanese-novel
	@cd games/japanese-novel && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/japanese-novel/{}" ';' && cd ../..
	@cd games/japanese-novel && find . -type f -exec install -v "$(DESTDIR)/share/suika2/japanese-novel/{}" "{}" ';' && cd ../..

	@install -v -d $(DESTDIR)/share/suika2/japanese-tategaki
	@cd games/japanese-tategaki && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/japanese-tategaki/{}" ';' && cd ../..
	@cd games/japanese-tategaki && find . -type f -exec install -v "{}" "$(DESTDIR)/share/suika2/japanese-tategaki/{}" ';' && cd ../..

	@install -v -d $(DESTDIR)/share/suika2/english
	@cd games/english && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/english/{}" ';' && cd ../..
	@cd games/english && find . -type f -exec install -v "{}" "$(DESTDIR)/share/suika2/english/{}" ';' && cd ../..

	@install -v -d $(DESTDIR)/share/suika2/english-novel
	@cd games/english-novel && find . -type d -exec install -v -d "$(DESTDIR)/share/suika2/english-novel/{}" ';' && cd ../..
	@cd games/english-novel && find . -type f -exec install -v "{}" "$(DESTDIR)/share/suika2/english-novel/{}" ';' && cd ../..

clean:
	rm -f suika-linux suika-pro

##
## dev internal
##

do-release:
	@cd build && ./do-release.sh && cd ..

setup:
	@# For macOS:
	@if [ ! -z "`uname | grep Darwin`" ]; then \
		if [ -z  "`which brew`" ]; then \
			echo 'Installing Homebrew...'; \
			/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"; \
		fi; \
		echo 'Installing build tools...'; \
		brew install mingw-w64 gsed cmake coreutils wget makensis create-dmg; \
		echo "Building libraries..."; \
		cd build/engine-windows && ./build-libs.sh && cd ../..; \
		cp -Ra build/engine-windows/libroot build/pro-windows/; \
	fi
	@# For Linux:
	@if [ ! -z "`uname | grep Linux`" ]; then \
		echo 'Installing dependencies...'; \
		sudo apt-get update; \
		sudo apt-get install build-essential cmake libasound2-dev libx11-dev libgstreamer1.0-dev libgstreamer-plugins-base1.0-dev libxpm-dev mesa-common-dev zlib1g-dev libpng-dev libjpeg-dev libwebp-dev libbz2-dev libogg-dev libvorbis-dev libfreetype-dev cmake qt6-base-dev qt6-multimedia-dev libqt6core6 libqt6gui6 libqt6widgets6 libqt6opengl6-dev libqt6openglwidgets6 libqt6multimedia6 libqt6multimediawidgets6 mingw-w64; \
	fi
	@# For WSL2:
	@if [ ! -z "`uname | grep WSL2`" ]; then \
		echo "Disabling EXE file execution."; \
		echo 0 | sudo tee /proc/sys/fs/binfmt_misc/WSLInterop; \
		cd build/engine-windows && ./build-libs.sh && cd ../..; \
		cp -Ra build/engine-windows/libroot build/pro-windows/; \
		echo "Re-enabling EXE file execution."; \
		echo 1 | sudo tee /proc/sys/fs/binfmt_misc/WSLInterop; \
	fi
	@# For FreeBSD:
	@if [ ! -z "`uname | grep FreeBS`" ]; then \
		echo 'Installing dependencies...'; \
		sudo pkg update; \
		sudo pkg install gmake gsed alsa-lib alsa-plugins qt6-6.6.2 xorg git cmake alsa-plugin mesa-devel freetype2; \
	fi

engine-windows:
	cd build/engine-windows && make && cd ../..

engine-windows-64:
	cd build/engine-windows-64 && make && cd ../..

engine-windows-arm64:
	cd build/engine-windows-arm64 && make && cd ../..

pro-windows:
	cd build/pro-windows && make && cd ../..

engine-macos:
	cd build/engine-macos && make && cd ../..

pro-macos:
	cd build/pro-macos && make && cd ../..

engine-wasm:
	cd build/engine-wasm  && make && cd ../..

pro-wasm:
	cd build/pro-wasm  && make && cd ../..

engine-linux: suika-linux

pro-linux: suika-pro
