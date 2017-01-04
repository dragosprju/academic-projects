from flask import Flask, render_template, request, url_for, redirect, session
from pygal import StackedBar
from pygal.style import Style
from datetime import timedelta, datetime
from operator import add
from draw import *

app = Flask(__name__, template_folder="pages", static_folder="scripts")

app.secret_key = "muchsecret"

error = None
success = None

def days_graph_with_values():
    (day_labels, values) = calc_graph_values()
    return days_graph(day_labels, values)

def days_graph(day_labels, values):
    custom_style = Style(
        background='transparent',
        font_family='Arial',
        colors=("#00CC00", "#CC0000", "#AAAAAA"),
    )

    range_max = max(map(add, values[0], values[1])) + 2

    line_chart = StackedBar(height=300, width=900, style=custom_style, range=(0,range_max))
    line_chart.add('Straight', values[0])
    line_chart.add('Bent', values[1])

    line_chart.x_labels = day_labels
    line_chart.y_labels = max(values[0]), range_max

    return line_chart.render_data_uri()

@app.route("/")
def amiloggedin():
    if "logged_in" in session:
        return redirect(url_for("dashboard"))
    else:
        return redirect(url_for("login"))

@app.route("/login", methods=['GET', 'POST'], strict_slashes=False)
def login():
    error = None
    success = None
    if "success" in session:
        success = session["success"]

    if request.method == "POST":
        if request.form["password"] != "1234":
            error = "Wrong password. Try again."
        else:
            session["logged_in"] = True
            return redirect(url_for("dashboard"))
    to_return = render_template("login.htm", error=error, success=success)
    session.pop("success", None)
    return to_return

@app.route("/logout", strict_slashes=False)
def logout():
    if "logged_in" in session:
        session.pop("logged_in", None)
        session["success"]="You have been logged out!"
    return redirect(url_for("login"))

@app.route("/dashboard", strict_slashes=False)
def dashboard():
    if (not ("logged_in" in session)):
        return redirect(url_for("login"))
    auto_refresh = None
    if "auto-refresh" in session:
        auto_refresh = session["auto-refresh"]
    graph_data = days_graph_with_values()

    [connected, threshold, bent] = get_settings()
    print "Connected: " + str(connected)

    return render_template("dashboard.htm", auto_refresh=auto_refresh, graph_data=graph_data, bent=bent, connected=connected, threshold=threshold)

@app.route("/dashboard/auto-refresh", strict_slashes=False)
def autorefresh():
    if (not ("logged_in" in session)):
        return redirect(url_for("login"))
    if "auto-refresh" in session:
        if session["auto-refresh"] == True:
            session["auto-refresh"] = False
        else:
            session["auto-refresh"] = True
    else:
        session["auto-refresh"] = True
    return redirect(url_for("dashboard"))