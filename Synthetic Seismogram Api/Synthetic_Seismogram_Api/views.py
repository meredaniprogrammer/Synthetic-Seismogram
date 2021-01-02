"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template
from Synthetic_Seismogram_Api import app
import numpy as np
from flask import request
@app.route('/')
@app.route('/home')
def home():
    """Renders the home page."""
    return render_template(
        'index.html',
        title='Home Page',
        year=datetime.now().year,
    )

def ricker(f, length, dt):
    t0 = np.arange(-length/2, (length-dt)/2, dt)
    y = (1.0 - 2.0*(np.pi**2)*(f**2)*(t0**2)) * np.exp(-(np.pi**2)*(f**2)*(t0**2))
    return t0, y



@app.route('/api/synthetic', methods=['GET'])
def synthetic():

    dt = request.args.get('dt')
    xp = request.args.get('xp')
    fp = request.args.get('fp')
    xpsplitlines=xp.split(",")
    fpsplitlines=fp.split(",")
    floatsx=[]
    floatsf=[]
    floatdt=float(dt)
    for x in xpsplitlines:
       floatx=  float(x)
       floatsx.append(floatx)

    for f in fpsplitlines:
       floatf=  float(f)
       floatsf.append(floatf)

    t_max = 3.0  
    t = np.arange(0, t_max, floatdt)
    AI_tdom = np.interp(t,floatsx,floatsf)    
   
    Rc_tdom = []
    for i in range(len(AI_tdom)-1):
        Rc_tdom.append((AI_tdom[i+1]-AI_tdom[i])/(AI_tdom[i]+AI_tdom[i+1]))

    f=20           
    length=0.512 
  
    t0, w = ricker(f, length, floatdt)
    synthetic = np.convolve(w, Rc_tdom, mode='same')
    xr="";
    for x in synthetic:
        newstring2= '{:.18f}'.format(x)
        newconcat=newstring2+","
        xr+=newconcat
        
    return xr


@app.route('/contact')
def contact():
    """Renders the contact page."""
    return render_template(
        'contact.html',
        title='Contact',
        year=datetime.now().year,
        message='Your contact page.'
    )

@app.route('/about')
def about():
    """Renders the about page."""
    return render_template(
        'about.html',
        title='About',
        year=datetime.now().year,
        message='Your application description page.'
    )
