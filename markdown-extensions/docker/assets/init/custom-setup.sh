#!/usr/bin/with-contenv bash

# Copy the theme template at first startup.
if [ -d /assets/template/themes/my-theme ] && [ ! -e /config/www/themes/my-theme ]; then
    echo Copy theme template
    mkdir -p /config/www/themes
    cp -RT /assets/template/themes/my-theme    /config/www/themes/my-theme
fi

# Link custom public files to the public location.
if [ -d /config/www/themes/my-theme/custom ]; then
    echo Link custom public dir
    ln -s /config/www/themes/my-theme/custom    /app/www/public/custom
fi

# Add theme setting
if [ -z "$(grep -e '^\s*APP_THEME\s*=' /config/www/.env)" ]; then
    echo Add theme setting
    echo "APP_THEME=my-theme" >> /config/www/.env
fi

