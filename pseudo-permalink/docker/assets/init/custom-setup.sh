#!/usr/bin/with-contenv bash

# Copy the theme template at first startup.
if [ -d /assets/template/themes/my-theme ] && [ ! -e /config/www/themes/my-theme ]; then
    echo Copy theme template
    mkdir -p /config/www/themes
    cp -RT /assets/template/themes/my-theme    /config/www/themes/my-theme
fi

# Add theme setting
if [ -z "$(grep -e '^\s*APP_THEME\s*=' /config/www/.env)" ]; then
    echo Add theme setting
    echo ""                    >> /config/www/.env
    echo "# Application theme" >> /config/www/.env
    echo "APP_THEME=my-theme"  >> /config/www/.env
fi
