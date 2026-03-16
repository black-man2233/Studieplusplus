import os
import sys
import time
from uptime_kuma_api import UptimeKumaApi, MonitorType

KUMA_URL   = "http://uptime-kuma:3001"
ADMIN_USER = "admin"
ADMIN_PASS = "admin123!"
DONE_FILE  = "/data/.setup_done"

MONITORS = [
    dict(
        type=MonitorType.HTTP,
        name="StudiePlusPlus API",
        url="http://studieplusplus.api:8080/scalar/",
        interval=30,
    ),
    dict(
        type=MonitorType.HTTP,
        name="Seq Logs",
        url="http://seq:80",
        interval=60,
    ),
    dict(
        type=MonitorType.PORT,
        name="SQL Server",
        hostname="sqlserver",
        port=1433,
        interval=60,
    ),
]


def connect(retries=30, delay=5):
    print(f"Connecting to Uptime Kuma at {KUMA_URL} ...")
    for i in range(1, retries + 1):
        try:
            api = UptimeKumaApi(KUMA_URL, wait_events=1, ssl_verify=False)
            print("Connected.")
            return api
        except Exception as exc:
            print(f"  [{i}/{retries}] not ready: {exc}")
            time.sleep(delay)
    print("Could not connect — giving up.")
    sys.exit(1)


def main():
    if os.path.exists(DONE_FILE):
        print("Setup marker found — nothing to do.")
        return

    api = connect()
    try:
        # Create admin account (only succeeds on a fresh instance)
        try:
            api.setup(ADMIN_USER, ADMIN_PASS)
            print(f"Admin user '{ADMIN_USER}' created.")
        except Exception as exc:
            print(f"setup() skipped ({exc}) — assuming user already exists.")

        api.login(ADMIN_USER, ADMIN_PASS)
        print("Logged in.")

        existing = {m["name"] for m in api.get_monitors()}

        for m in MONITORS:
            if m["name"] in existing:
                print(f"  skip (exists): {m['name']}")
            else:
                api.add_monitor(**m)
                print(f"  added: {m['name']}")

        # Write marker so we don't run again
        with open(DONE_FILE, "w") as f:
            f.write("done\n")

        print("Setup complete.")
    finally:
        api.disconnect()


if __name__ == "__main__":
    main()
